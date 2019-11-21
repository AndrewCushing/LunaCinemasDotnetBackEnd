using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AccountCreationService _accountCreationService;
        private readonly ExistingUserService _existingUserService;

        public UserController(AccountCreationService accountCreationService, ExistingUserService existingUserService)
        {
            _accountCreationService = accountCreationService;
            _existingUserService = existingUserService;
        }

        [Route("addcustomer")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddCustomer([Microsoft.AspNetCore.Mvc.FromBody] List<string> customerDetails)
        {
            return _accountCreationService.AddCustomer(customerDetails);
        }

        [Route("addstaff")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddStaff([Microsoft.AspNetCore.Mvc.FromBody] List<string> adminDetails)
        {
            return _accountCreationService.AddAdmin(adminDetails);
        }

        [Route("login")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AttemptLogin([Microsoft.AspNetCore.Mvc.FromBody] List<string> usernameAndPassword)
        {
            return _existingUserService.Login(usernameAndPassword);
        }

        [Route("verify/{token}")]
        [HttpGet]
        public ActionResult<ResponseObject<string>> VerifyAccessToken([FromRoute] string token)
        {
            return _existingUserService.VerifyToken(token);
        }

        [Route("logout/{token}")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> Logout([FromRoute] string token)
        {
            return _existingUserService.Logout(token);
        }

        [Route("delete")]
        [HttpPost]
        public ActionResult<ResponseObject<object>> DeleteUser([FromBody] string[] usernameAndPassword)
        {
            return null;
        }

        [Route("changepassword")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> ChangePassword([FromBody] string[] tokenOldNewPasswords)
        {
            return null;
        }
    }
}