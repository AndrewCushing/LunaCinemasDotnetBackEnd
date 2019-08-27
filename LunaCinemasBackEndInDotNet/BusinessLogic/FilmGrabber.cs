using System;
using System.Collections.Generic;
using System.Linq;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class FilmGrabber
    {
        private readonly IMongoCollection<Film> _films;

        public FilmGrabber(ILunaCinemasDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _films = database.GetCollection<Film>(settings.FilmsCollectionName);
        }

        public ActionResult<List<Film>> GetNew()
        {
            var data = _films.Find(film => film.IsReleased).ToList();
            ResponseObject<Film> res = new ResponseObject<Film>(true, "Retrieved all newly released films", new List<Film>(data));
            return data;
        }

        public ActionResult<List<Film>> GetUpcoming()
        {
            var data = _films.Find(film => !film.IsReleased).ToList();
            return data;
        }
            

        public Film GetById(string id) =>
            _films.Find<Film>(film => film.Id == id).FirstOrDefault();

        public ActionResult<List<Film>> SearchFilms(string searchQuery)
        {
            var data = _films.Find(film => SearchThisFilm(film, searchQuery)).ToList();
            return data;
        }

        private bool SearchThisFilm(Film film, string searchQuery)
        {
            return film.Title.Contains(searchQuery) ||
                   film.Genres.ToList().Find(genre => genre.Contains(searchQuery)).Length > 0 ||
                   film.Actors.ToList().Find(actor => actor.Contains(searchQuery)).Length > 0 ||
                   film.Directors.ToList().Find(director => director.Contains(searchQuery)).Length > 0;
        }
    }
}