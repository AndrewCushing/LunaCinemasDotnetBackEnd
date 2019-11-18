using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly UserService _businessware;

        public UserController(UserService businessware)
        {
            _businessware = businessware;
        }

        [Route("addcustomer")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddCustomer([FromBody] List<string> customerDetails)
        {
            return _businessware.AddCustomer(customerDetails);
        }

        [Route("addstaff")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddStaff([FromBody] List<string> adminDetails)
        {
            return _businessware.AddAdmin(adminDetails);
        }

        [Route("login")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AttemptLogin([FromBody] List<string> usernameAndPassword)
        {
            return _businessware.Login(usernameAndPassword);
        }

        [Route("verify")]
        [HttpPost]
        public ActionResult<ResponseObject<object>> VerifyAccessToken([FromBody] string token)
        {
            return null;
        }

        [Route("logout")]
        [HttpPost]
        public ActionResult<ResponseObject<object>> Logout([FromBody] string token)
        {
            return null;
        }

        [Route("delete")]
        [HttpPost]
        public ActionResult<ResponseObject<object>> DeleteUser([FromBody] string[] usernameAndPassword)
        {
            return null;
        }
    }
}