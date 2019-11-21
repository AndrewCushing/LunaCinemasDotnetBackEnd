using System;
using System.Collections.Generic;
using System.IO;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class UserService
    {
        private readonly ICustomerContext _customerContext;
        private readonly SecurityService _securityService;
        private readonly IAdminContext _adminContext;

        public UserService(ICustomerContext customerContext, SecurityService securityService, IAdminContext adminContext)
        {
            _customerContext = customerContext;
            _securityService = securityService;
            _adminContext = adminContext;
        }

        public ResponseObject<string> AddCustomer(List<string> customerDetails)
        {
            return AddUser(typeof(Customer), customerDetails);
        }

        public ResponseObject<string> AddAdmin(List<string> adminDetails)
        {
            return AddUser(typeof(Admin), adminDetails);
        }

        private bool VerifyUserDetailsAreValid(User user)
        {
            return true;
        }

        private ResponseObject<string> AddUser(Type type, List<string> details)
        {
            bool success = false;
            string reason;
            List<string> content = null;
            try
            {
                User user = CreateUser(type, details);
                if (VerifyUserDetailsAreValid(user))
                {
                    SaveUser(user);
                    string newToken = _securityService.GetNewToken(GetUserId(type, details[2]));
                    success = true;
                    reason = $"{type.Name} account created successfully";
                    content = new List<string> { newToken };
                }
                else
                {
                    reason = $"Unable to create {type.Name} account. Details are invalid.";
                }
            }
            catch (InvalidDataException)
            {
                reason = $"Unable to create {type.Name} account. Password was not hashed correctly";
            }
            return new ResponseObject<string>(success, reason, content);
        }

        private string GetUserId(Type type, string email)
        {
            if (type.IsEquivalentTo(typeof(Customer)))
            {
                return _customerContext.FindByEmail(email)[0].Id;
            }
            return _adminContext.FindByEmail(email)[0].Id;
        }

        private void SaveUser(User user)
        {
            if (user.GetType().IsEquivalentTo(typeof(Customer)))
            {
                _customerContext.Save((Customer)user);
            }
            else
            {
                _adminContext.Create((Admin)user);
            }
        }

        private User CreateUser(Type type, List<string> details)
        {
            if (type.IsEquivalentTo(typeof(Customer)))
            {
                return new Customer(details[0], details[1], details[2], details[3]);
            }
            return new Admin(details[0], details[1], details[2], details[3]);
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
            return new ResponseObject<string>(true, "Login successful", new List<string>{ _securityService.GetNewToken(id) });
        }

        public ResponseObject<string> VerifyToken(string accessTokenId)
        {
            if (_securityService.ValidateToken(accessTokenId))
            {
                return new ResponseObject<string>(true, "Access token is valid and has been refreshed.", new List<string>{accessTokenId});
            }
            return new ResponseObject<string>(false, "Unable to verify access token", null);
        } 
    }
}