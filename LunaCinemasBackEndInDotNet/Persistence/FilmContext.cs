using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        void DeleteAll();
        void AddFilm(Film filmToAdd);
        Film FindByTitle(string title);
        void UpdateFilm(Film film);
        void DeleteFilm(string filmId);
    }
    
    [ExcludeFromCodeCoverage]
    public class FilmContext : IFilmContext
    {
        private readonly IMongoCollection<Film> _filmCollection;
        public FilmContext(ILunaCinemasDatabaseSettings settings)
        {
            IMongoClient client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            _filmCollection = db.GetCollection<Film>(settings.FilmsCollectionName);
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

        public void DeleteAll()
        {
            _filmCollection.DeleteMany(film => true);
        }

        public void AddFilm(Film filmToAdd)
        {
            _filmCollection.InsertOne(filmToAdd);
        }

        public Film FindByTitle(string title)
        {
            return _filmCollection.Find(film => film.Title.Equals(title)).ToList()[0];
        }

        public void UpdateFilm(Film film)
        {
            _filmCollection.ReplaceOneAsync(oldFilm => oldFilm.Id == film.Id, film);
        }

        public void DeleteFilm(string filmId)
        {
            _filmCollection.DeleteOne(film => film.Id == filmId);
        }
    }
}