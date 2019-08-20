using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
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

            FilmContext db = new FilmContext(null);
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