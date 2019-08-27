using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly FilmGrabber _businessWare;

        public FilmController(FilmGrabber _businessware)
        {
            _businessWare = _businessware;
        }

        [Route("getnewfilms")]
        [HttpGet]
        public ActionResult<List<Film>> GetNewFilms()
        {
            return _businessWare.GetNew();
        }

        [Route("getupcomingfilms")]
        [HttpGet]
        public ActionResult<List<Film>> GetUpcomingFilms()
        {
            return _businessWare.GetUpcoming();
        }

        [Route("search/{searchQuery}")]
        [HttpGet]
        public ActionResult<List<Film>> SearchFilms([FromRoute] string searchQuery)
        {
            return _businessWare.SearchFilms(searchQuery);
        }

    }
}