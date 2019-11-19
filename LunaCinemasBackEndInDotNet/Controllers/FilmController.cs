using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly FilmService _businessware;

        public FilmController(FilmService businessware)
        {
            _businessware = businessware;
        }

        [Route("getnewfilms")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> GetNewFilms()
        {
            return _businessware.GetNew();
        }

        [Route("getfilm/{id}")]
        [HttpGet("id")]
        public ActionResult<ResponseObject<Film>> GetFilm(string id)
        {
            return _businessware.GetById(id);
        }

        [Route("getupcomingfilms")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> GetUpcomingFilms()
        {
            return _businessware.GetUpcoming();
        }

        [Route("search/{searchQuery}")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> SearchFilms([FromRoute] string searchQuery)
        {
            return _businessware.SearchFilms(searchQuery);
        }

        [Route("add")]
        [HttpPost]
        public ActionResult<ResponseObject<Film>> AddFilm([FromBody] Film film)
        {
            return _businessware.AddFilm(film);
        }

        [Route("update")]
        [HttpPost]
        public ActionResult<ResponseObject<Film>> UpdateFilm([FromBody] Film film)
        {
            return _businessware.UpdateFilm(film);
        }

        [Route("delete/{filmId}")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> DeleteFilm(string filmId)
        {
            return _businessware.DeleteFilm(filmId);
        }
    }
}