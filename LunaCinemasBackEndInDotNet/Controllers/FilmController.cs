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
        private readonly FilmGrabber _businessWare;

        public FilmController(FilmGrabber _businessware)
        {
            _businessWare = _businessware;
        }

        [Route("getallfilms")]
        [HttpGet]
        public ActionResult<List<Film>> GetAllFilms()
        {
            return _businessWare.Get();
        }

        //[Route("searchfilms/{searchText}")]
        //[HttpGet]
        //public ActionResult<ResponseObject<Film>> SearchFilms(string searchText)
        //{
        //    //todo
        //    return _businessWare.Get();
        //}

        //[Route("getfilm/{id}")]
        //[HttpGet]
        //public ActionResult<ResponseObject<Film>> GetFilm(string id)
        //{
        //    //todo
        //    return _businessWare.Get();
        //}

        //[Route("getupcomingfilms")]
        //[HttpGet]
        //public ActionResult<ResponseObject<Film>> GetUpcomingFilms()
        //{
        //    //todo
        //    return _businessWare.Get();
        //}

        //[Route("getnewfilms")]
        //[HttpGet]
        //public ActionResult<ResponseObject<Film>> GetnewFilms()
        //{
        //    //todo
        //    return _businessWare.Get();
        //}

        //[Route("check")]
        //[HttpGet]
        //public ActionResult<ResponseObject<Film>> Check()
        //{
        //    return _businessWare.Get();
        //}

    }
}