using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly FilmGrabber _businessWare = new FilmGrabber();

        [Route("getallfilms")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> GetAllFilms()
        {
            return _businessWare.GetAll();
        }

        [Route("searchfilms/{searchText}")]
        [HttpGet]
        public ActionResult<ResponseObject<Film>> SearchFilms(string searchText)
        {
            //todo
            return _businessWare.Search(searchText);
        }

        [Route("getfilm/{id}")]
        [HttpGet]
        public ActionResult<string> GetFilm(string id)
        {
            //todo
            return $"you got the film with an id of {id}";
        }

        [Route("getupcomingfilms")]
        [HttpGet]
        public ActionResult<string> GetUpcomingFilms()
        {
            //todo
            return $"you got all the upcoming films";
        }

        [Route("getnewfilms")]
        [HttpGet]
        public ActionResult<string> GetnewFilms()
        {
            //todo
            return "you got all the new films";
        }

        [Route("check")]
        [HttpGet]
        public ActionResult<string> Check()
        {
            FilmContext filmContext = new FilmContext();
            return filmContext.checkDb();
        }

    }
}