using System.Collections.Generic;
using System.IO;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.IdGenerators;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class UserService
    {
        private readonly ICustomerContext _customerContext;
        private readonly SecurityService _securityService;

        public UserService(ICustomerContext customerContext, SecurityService securityService)
        {
            _customerContext = customerContext;
            _securityService = securityService;
        }

        public ActionResult<ResponseObject<string>> AddCustomer(List<string> customerDetails)
        {
            bool success = false;
            string reason;
            List<string> content = null;
            try
            {
                Customer customer = new Customer(customerDetails[0], customerDetails[1], customerDetails[2],
                    customerDetails[3]);
                if (VerifyCustomerDetailsAreValid(customer))
                {
                    _customerContext.SaveUser(customer);
                    string newToken = _securityService.GetNewToken(_customerContext.FindByEmail(customer.Email)[0].Id);
                    success = true;
                    reason = "Customer account created successfully";
                    content = new List<string> { newToken };
                }
                else
                {
                    reason = "Unable to create customer account. Details are invalid.";
                }
            }
            catch (InvalidDataException)
            {
                reason = "Unable to create customer account. Password was not hashed correctly";
            }
            return new ResponseObject<string>(success, reason, content);
        }

        private static bool VerifyCustomerDetailsAreValid(Customer customer)
        {
            return true;
        }
    }
}