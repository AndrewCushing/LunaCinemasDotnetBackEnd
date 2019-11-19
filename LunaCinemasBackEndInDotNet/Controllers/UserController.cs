using System.Collections.Generic;
using System.Web.Http;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly AccountCreationService _accountCreationService;
        private readonly ExistingUserService _existingUserService;

        public UserController(AccountCreationService accountCreationService, ExistingUserService existingUserService)
        {
            _accountCreationService = accountCreationService;
            _existingUserService = existingUserService;
        }

        [Microsoft.AspNetCore.Mvc.Route("addcustomer")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<ResponseObject<string>> AddCustomer([Microsoft.AspNetCore.Mvc.FromBody] List<string> customerDetails)
        {
            return _accountCreationService.AddCustomer(customerDetails);
        }

        [Microsoft.AspNetCore.Mvc.Route("addstaff")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<ResponseObject<string>> AddStaff([Microsoft.AspNetCore.Mvc.FromBody] List<string> adminDetails)
        {
            return _accountCreationService.AddAdmin(adminDetails);
        }

        [Microsoft.AspNetCore.Mvc.Route("login")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<ResponseObject<string>> AttemptLogin([Microsoft.AspNetCore.Mvc.FromBody] List<string> usernameAndPassword)
        {
            return _existingUserService.Login(usernameAndPassword);
        }

        [Microsoft.AspNetCore.Mvc.Route("verify/{token}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<ResponseObject<string>> VerifyAccessToken([FromUri] string token)
        {
            return _existingUserService.VerifyToken(token);
        }

        [Microsoft.AspNetCore.Mvc.Route("logout")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<ResponseObject<object>> Logout([Microsoft.AspNetCore.Mvc.FromBody] string token)
        {
            return null;
        }

        [Microsoft.AspNetCore.Mvc.Route("delete")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<ResponseObject<object>> DeleteUser([Microsoft.AspNetCore.Mvc.FromBody] string[] usernameAndPassword)
        {
            return null;
        }
    }
}