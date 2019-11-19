using System;
using System.Collections.Generic;
using System.Linq;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class FilmService
    {
        private readonly IFilmContext _filmContext;

        public FilmService(IFilmContext filmContext)
        {
            _filmContext = filmContext;
        }

        public ActionResult<ResponseObject<Film>> GetNew()
        {
            ActionResult<ResponseObject<Film>> res;
            try
            {
                res = new ResponseObject<Film>(true, ResponseText.SuccessfullyRetrievedNewFilms, new List<Film>(_filmContext.GetReleasedFilms()));
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
                var data = _filmContext.GetUpcomingFilms();
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
            ActionResult<ResponseObject<Film>> res = new ResponseObject<Film>(true, "Film data retrieved", new List<Film>(_filmContext.FindById(id)));
            return res;
        }

        public ActionResult<ResponseObject<Film>> SearchFilms(string searchQuery)
        {
            List<Film> data = _filmContext.GetAllFilms();
            int j = data.Count;
            for (int i = 0 ; i < j ; i++)
            {
                if (!SearchThisFilm(data[i], searchQuery))
                {
                    data.RemoveAt(i);
                    j--;
                    i--;
                }
            }
            return new ResponseObject<Film>(true, $"Search complete. Found {data.Count} films", data);
        }

        public ActionResult<ResponseObject<Film>> AddFilm(Film film)
        {
            _filmContext.AddFilm(film);
            return new ResponseObject<Film>(true, "Film successfully added to database.", _filmContext.GetAllFilms());
        }

        public ActionResult<ResponseObject<Film>> UpdateFilm(Film film)
        {
            _filmContext.UpdateFilm(film);
            return new ResponseObject<Film>(true, "Film updated", _filmContext.GetAllFilms());
        }

        public ActionResult<ResponseObject<Film>> DeleteFilm(string filmId)
        {
            _filmContext.DeleteFilm(filmId);
            return new ResponseObject<Film>(true, "Film deleted", _filmContext.GetAllFilms());
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