using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IFilmContext
    {
        List<Film> FindById(string filmId);
        List<Film> GetReleasedFilms();
        List<Film> GetUpcomingFilms();
        List<Film> GetAllFilms();
    }
    public class FilmContext : IFilmContext
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<Film> _filmCollection;
        public FilmContext(ILunaCinemasDatabaseSettings settings)
        {
            IMongoClient client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DatabaseName);
            _filmCollection = _db.GetCollection<Film>(settings.FilmsCollectionName);
        }
        
        public List<Film> FindById(string filmId)
        {
            return _filmCollection.Find(filmDoc => filmDoc.Id.Equals(filmId)).ToList();
        }

        public List<Film> GetReleasedFilms()
        {
            return _filmCollection.Find(film => film.IsReleased).ToList();
        }

        public List<Film> GetUpcomingFilms()
        {
            return _filmCollection.Find(film => !film.IsReleased).ToList();
        }

        public List<Film> GetAllFilms()
        {
            return _filmCollection.Find(film => true).ToList();
        }
    }
}