using System.Collections.Generic;
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
            if (VerifyCustomerDetailsAreValid(customer))
            {
                string newToken = _securityService.GetNewToken(customer.Id);
                _userContext.SaveUser(customer);
                return new ResponseObject<string>(true, "Customer account created successfully", new List<string>{newToken});
            }
            return new ResponseObject<string>(false, "Unable to create customer account. Details are invalid.", null);
        }

        private static bool VerifyCustomerDetailsAreValid(Customer customer)
        {
            return true;
        }
    }
}