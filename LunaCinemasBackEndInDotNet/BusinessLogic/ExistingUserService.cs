using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Persistence;

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
    }
}