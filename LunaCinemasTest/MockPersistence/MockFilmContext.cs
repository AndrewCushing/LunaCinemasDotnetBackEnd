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
        public List<Film> FindById(string filmId)
        {
            List<Film> result = new List<Film>();
            result.Add(_findByIdResults[filmId]);
            return result;
        }

        public void AddFindByIdResult(Film filmToReturn)
        {
            _findByIdResults[filmToReturn.Id] = filmToReturn;
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

        public void AddToGetAllFilmResults(List<Film> filmsToAdd)
        {
            _getAllFilmsResults.AddRange(filmsToAdd);
        }
    }
}