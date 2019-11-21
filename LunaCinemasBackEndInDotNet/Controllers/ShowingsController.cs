using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowingsController : Controller
    {
        private readonly ShowingService _businessware;

        public ShowingsController(ShowingService businessware)
        {
            _businessware = businessware;
        }
        
        [Route("getshowings/{filmId}")]
        [HttpGet]
        public ActionResult<ResponseObject<object>> GetShowingsByFilmId(string filmId)
        {
            return _businessware.GetShowingsByFilmId(filmId);
        }
    }
}