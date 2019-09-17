using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockFilmContext : IFilmContext
    {
        private readonly Dictionary<string, List<Film>> _findByIdResults = new Dictionary<string, List<Film>>();
        private readonly List<Film> _getReleasedFilmsResults = new List<Film>();
        private readonly List<Film> _getUpcomingFilmsResults = new List<Film>();
        private readonly List<Film> _getAllFilmsResults = new List<Film>();
        public List<Film> FindById(string filmId)
        {
            return _findByIdResults[filmId];
        }

        public void AddFindByIdResult(string filmId, List<Film> result)
        {
            _findByIdResults[filmId] = result;
        }

        public List<Film> GetReleasedFilms()
        {
            return _getReleasedFilmsResults;
        }

        public void AddToGetReleasedFilmsResults(List<Film> filmsToAdd)
        {
            _getReleasedFilmsResults.AddRange(filmsToAdd);
        }

        public List<Film> GetUpcomingFilms()
        {
            return _getUpcomingFilmsResults;
        }

        public void AddToGetUpcomingFilmsResults(List<Film> filmsToAdd)
        {
            _getUpcomingFilmsResults.AddRange(filmsToAdd);
        }

        public List<Film> GetAllFilms()
        {
            return _getAllFilmsResults;
        }

        public void AddToAllGetFilmResults(List<Film> filmsToAdd)
        {
            _getAllFilmsResults.AddRange(filmsToAdd);
        }
    }
}