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
        public ActionResult<ResponseObject<string>> AddCustomer([FromBody] Customer customer)
        {
            return null;
        }

        [Route("addstaff")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddStaff([FromBody] Admin admin)
        {
            return null;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AttemptLogin([FromBody] string[] usernameAndPassword)
        {
            return null;
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