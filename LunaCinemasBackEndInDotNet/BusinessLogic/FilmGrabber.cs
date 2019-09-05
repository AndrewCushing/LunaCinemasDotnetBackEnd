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
            ActionResult<ResponseObject<Film>> res;
            try
            {
                UpdateFilms();
                var data = _films.Find(film => film.IsReleased).ToList();
                res = new ResponseObject<Film>(true, ResponseText.SuccessfullyRetrievedNewFilms, new List<Film>(data));
                return res;
            }
            catch (Exception)
            {
                res = new ResponseObject<Film>(false, ResponseText.UnableToRetrieveNewFilms, null);
                return res;
            }
        }

        public ActionResult<ResponseObject<Film>> GetUpcoming()
        {
            ActionResult<ResponseObject<Film>> res;
            try
            {
                UpdateFilms();
                var data = _films.Find(film => !film.IsReleased).ToList();
                res = new ResponseObject<Film>(true, ResponseText.SuccessfullyRetrievedUpcomingFilms, new List<Film>(data));
                return res;
            }
            catch (Exception)
            {
                res = new ResponseObject<Film>(false, ResponseText.UnableToRetrieveUpcomingFilms, null);
                return res;
            }
        }
            

        public ActionResult<ResponseObject<Film>> GetById(string id)
        {
            UpdateFilms();
            var data = _films.Find(film => film.Id.Equals(id)).ToList();
            ActionResult<ResponseObject<Film>> res = new ResponseObject<Film>(true, "Retrieved all newly released films", new List<Film>(data));
            return res;
        }

        public ActionResult<ResponseObject<Film>> SearchFilms(string searchQuery)
        {
            UpdateFilms();
            List<Film> data = _films.Find(film => true).ToList();
            int j = data.Count;
            for (int i = 0 ; i < j ; i++)
            {
                if (!SearchThisFilm(data[i], searchQuery))
                {
                    data.Remove(data[i]);
                    j--;
                    i--;
                }
            }
            return new ResponseObject<Film>(true, $"Search complete. Found {data.Count} films", data);
        }

        private bool SearchThisFilm(Film film, string searchQuery)
        {
            return film.Title.ToLower().Contains(searchQuery.ToLower()) ||
                   film.Genres.ToList().Find(genre => genre.ToLower().Contains(searchQuery.ToLower())) != null ||
                   film.Actors.ToList().Find(actor => actor.ToLower().Contains(searchQuery.ToLower())) != null ||
                   film.Directors.ToList().Find(director => director.ToLower().Contains(searchQuery.ToLower())) != null;
        }
    }
}