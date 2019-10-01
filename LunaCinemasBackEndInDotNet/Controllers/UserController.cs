using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly UserHandler _businessware;

        public UserController(UserHandler businessware)
        {
            _businessware = businessware;
        }

        [Route("add")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddUser([FromBody] User user)
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