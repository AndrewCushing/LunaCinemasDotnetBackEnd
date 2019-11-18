using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class UserService
    {
        private readonly IUserContext _userContext;
        private readonly SecurityService _securityService;

        public UserService(IUserContext userContext, SecurityService securityService)
        {
            _userContext = userContext;
            _securityService = securityService;
        }


        public ActionResult<ResponseObject<string>> AddCustomer(Customer customer)
        {
            string newToken = _securityService.GetNewToken(customer.Id);

            return null;
        }
    }
}