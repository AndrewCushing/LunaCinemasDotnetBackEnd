using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowingsController : ControllerBase
    {
        private readonly ShowingHandler _businessware;

        public ShowingsController(ShowingHandler businessware)
        {
            _businessware = businessware;
        }
        
        [Route("getshowings/{filmId}")]
        [HttpGet("filmId")]
        public ActionResult<ResponseObject<object>> GetShowingsByFilmId([FromRoute] string filmId)
        {
            return _businessware.GetShowingsByFilmId(filmId);
        }
    }
}