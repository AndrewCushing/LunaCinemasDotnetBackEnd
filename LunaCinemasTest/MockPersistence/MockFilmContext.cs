using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockFilmContext : IFilmContext
    {
        private readonly Dictionary<string, Film> _findByIdResults = new Dictionary<string, Film>();
        private readonly List<Film> _getReleasedFilmsResults = new List<Film>();
        private readonly List<Film> _getUpcomingFilmsResults = new List<Film>();
        private readonly List<Film> _getAllFilmsResults = new List<Film>();
        private bool _broken;
        public List<Film> FindById(string filmId)
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            List<Film> result = new List<Film>();
            result.Add(_findByIdResults[filmId]);
            return result;
        }

        public void AddFindByIdResult(Film filmToReturn)
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            _findByIdResults[filmToReturn.Id] = filmToReturn;
        }

        public List<Film> GetReleasedFilms()
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            return _getReleasedFilmsResults;
        }

        public void AddToGetReleasedFilmsResults(List<Film> filmsToAdd)
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            _getReleasedFilmsResults.AddRange(filmsToAdd);
        }

        public List<Film> GetUpcomingFilms()
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            return _getUpcomingFilmsResults;
        }

        public void AddToGetUpcomingFilmsResults(List<Film> filmsToAdd)
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            _getUpcomingFilmsResults.AddRange(filmsToAdd);
        }

        public List<Film> GetAllFilms()
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            return _getAllFilmsResults;
        }

        public void AddToGetAllFilmResults(List<Film> filmsToAdd)
        {
            if (_broken)
            {
                throw new Exception("Can't access data");
            }
            _getAllFilmsResults.AddRange(filmsToAdd);
        }

        public void BreakPersistence()
        {
            _broken = true;
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void AddFilm(Film filmToAdd)
        {
            throw new NotImplementedException();
        }

        public Film FindByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}