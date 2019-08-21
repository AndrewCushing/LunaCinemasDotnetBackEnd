using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        [Route("check")]
        [HttpGet]
        public ActionResult<string> Check()
        {
            FilmContext filmContext = new FilmContext();
            return filmContext.checkDb();
        }

        [Route("getallfilms")]
        [HttpGet]
        public ActionResult<string> GetAllFilms()
        {
            //todo
            return "you got all the films";
        }

        [Route("searchfilms")]
        [HttpGet]
        public ActionResult<string> SearchFilms()
        {
            //todo
            return "you searched for some films";
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

    }
}