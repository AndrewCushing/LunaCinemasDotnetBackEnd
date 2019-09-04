using System.Collections.Generic;
using System.Linq;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class FilmGrabber
    {
        private IMongoCollection<Film> _films;
        private readonly ILunaCinemasDatabaseSettings _settings;

        public FilmGrabber(ILunaCinemasDatabaseSettings settings)
        {
            _settings = settings;
            UpdateFilms();
        }

        public Film GrabFilmObject(string filmId)
        {
            UpdateFilms();
            var data = _films.Find(film => film.Id.Equals(filmId)).ToList();
            if (data.Count < 1)
            {
                return null;
            }

            return data[0];
        }

        private void UpdateFilms()
        {
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _films = database.GetCollection<Film>(_settings.FilmsCollectionName);
        }

        public ActionResult<ResponseObject<Film>> GetNew()
        {
            UpdateFilms();
            var data = _films.Find(film => film.IsReleased).ToList();
            ActionResult<ResponseObject<Film>> res = new ResponseObject<Film>(true, "Retrieved all newly released films", new List<Film>(data));
            return res;
        }

        public ActionResult<ResponseObject<Film>> GetUpcoming()
        {
            UpdateFilms();
            var data = _films.Find(film => !film.IsReleased).ToList();
            ActionResult<ResponseObject<Film>> res = new ResponseObject<Film>(true, "Retrieved all newly released films", new List<Film>(data));
            return res;
        }
            

        public ActionResult<ResponseObject<Film>> GetById(string id)
        {
            UpdateFilms();
            var data = _films.Find(film => film.Id.Equals(id)).ToList();
            ActionResult<ResponseObject<Film>> res = new ResponseObject<Film>(true, "Retrieved all newly released films", new List<Film>(data));
            return res;
        }

        public ActionResult<List<Film>> SearchFilms(string searchQuery)
        {
            UpdateFilms();
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