using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowingsController : ControllerBase
    {
        private readonly ShowingService _businessware;

        public ShowingsController(ShowingService businessware)
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