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
        public ActionResult<ResponseObject<Film>> GetNewFilms()
        {
            return _businessWare.GetNew();
        }

        [Route("getfilm/{id}")]
        [HttpGet("id")]
        public ActionResult<ResponseObject<Film>> GetFilm(string id)
        {
            return _businessWare.GetById(id);
        }

        [Route("getupcomingfilms")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> GetUpcomingFilms()
        {
            return _businessWare.GetUpcoming();
        }

        [Route("search/{searchQuery}")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> SearchFilms([FromRoute] string searchQuery)
        {
            return _businessWare.SearchFilms(searchQuery);
        }

    }
}