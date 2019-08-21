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

        // GET: Film
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new [] {"Put a film here", "Put another film here", "etc"};
        }

        [Route("add/{id}/{title}/{briefDescription}/{length}/{year}/{classification}")]
        [HttpPut]
        public ActionResult<bool> Add(int id, string title, string briefDescription, string length, string year, string classification)
        {
            Film filmToAdd = new Film
            {
                Id = id,
                Title = title,
                BriefDescription = briefDescription,
                Length = length,
                Year = year,
                Classification = classification
            };

            return true;
        }

        [Route("getall")]
        public ActionResult<IEnumerable<Film>> GetAllFilms()
        {
            //todo
            return null;
        }
        
    }
}