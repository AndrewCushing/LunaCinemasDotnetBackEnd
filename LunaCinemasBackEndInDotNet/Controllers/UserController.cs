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

        [Route("adduser")]
        [HttpPost]
        public ActionResult<ResponseObject<string>> AddUser([FromBody] User user)
        {
            return null;
        }

        public ActionResult<ResponseObject<string>> AttemptLogin([FromBody] string username, [FromBody] string password)
        {
            return null;
        }

        public ActionResult<ResponseObject<object>> VerifyAccessToken([FromBody] string token)
        {
            return null;
        }

        public ActionResult<ResponseObject<object>> Logout([FromBody] string token)
        {
            return null;
        }

        public ActionResult<ResponseObject<object>> DeleteUser([FromBody] string username, [FromBody] string password)
        {
            return null;
        }
    }
}