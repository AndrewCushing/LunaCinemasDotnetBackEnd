using System.Collections.Generic;
using System.ComponentModel;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ExistingUserService
    {
        private readonly ICustomerContext _customerContext;
        private readonly SecurityService _securityService;
        private readonly IAdminContext _adminContext;

        public ExistingUserService(ICustomerContext customerContext, SecurityService securityService, IAdminContext adminContext)
        {
            _customerContext = customerContext;
            _securityService = securityService;
            _adminContext = adminContext;
        }

        public ResponseObject<string> Login(List<string> usernameAndPassword)
        {
            string customerId = _customerContext.FindByEmailAndPassword(usernameAndPassword[0], usernameAndPassword[1]);
            string adminId = _adminContext.FindByEmailAndPassword(usernameAndPassword[0], usernameAndPassword[1]);
            if (customerId == null && adminId == null)
            {
                return new ResponseObject<string>(false, "Username or password is incorrect", null);
            }
            string id = customerId ?? adminId;
            return new ResponseObject<string>(true, "Login successful", new List<string> { _securityService.GetNewToken(id) });
        }

        public ResponseObject<string> VerifyToken(string accessTokenId)
        {
            if (_securityService.ValidateToken(accessTokenId))
            {
                return new ResponseObject<string>(true, "Access token is valid and has been refreshed.", new List<string> { accessTokenId });
            }
            return new ResponseObject<string>(false, "Unable to verify access token", null);
        }

        public ActionResult<ResponseObject<string>> Logout(string token)
        {
            _securityService.DeleteToken(token);
            return new ResponseObject<string>(true, "User is now logged out", null);
        }

        public ActionResult<ResponseObject<string>> DeleteUser(string[] tokenAndPassword)
        {
            if (!_securityService.ValidateToken(tokenAndPassword[0]))
                return new ResponseObject<string>(false, "Access token is not valid", null);

            User user = FetchUserUsingToken(tokenAndPassword[0]);
            if (user.Password == tokenAndPassword[1])
            {
                if (user is Admin)
                    _adminContext.DeleteAdmin(user.Id);
                else
                    _customerContext.DeleteCustomer(user.Id);

                _securityService.DeleteToken(tokenAndPassword[0]);
                return new ResponseObject<string>(true, "User deleted", null);
            }
            return new ResponseObject<string>(false, "Password given does not match existing password", null);
        }

        public ActionResult<ResponseObject<string>> ChangePassword(string[] tokenOldNewPasswords)
        {
            if (!_securityService.ValidateToken(tokenOldNewPasswords[0]))
                return new ResponseObject<string>(false, "Access token is not valid", null);
            
            User user = FetchUserUsingToken(tokenOldNewPasswords[0]);
            if (tokenOldNewPasswords[1] != user.Password)
                return new ResponseObject<string>(false, "Password given does not match current password", null);

            if (user is Admin)
            {
                _adminContext.ChangePassword(user.Id,tokenOldNewPasswords[2]);
            }
            else
            {
                _customerContext.ChangePassword(user.Id, tokenOldNewPasswords[2]);
            }
            return new ResponseObject<string>(true, "Password changed successfully", null);
        }

        private User FetchUserUsingToken(string token)
        {
            string userId = _securityService.GetUserIdFromToken(token);
            Admin possibleAdmin = _adminContext.FindById(userId);
            User user = possibleAdmin ?? (User)_customerContext.FindById(userId);
            return user;
        }
    }
}