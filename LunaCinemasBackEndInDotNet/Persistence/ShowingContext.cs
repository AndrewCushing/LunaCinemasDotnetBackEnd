using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IShowingContext
    {
        List<Showing> GetByFilmId(string filmId);
        Showing GetById(string id);
        bool UpdateShowing(Showing oldShowing, Showing newShowing);
        void DeleteAll();
        void AddShowing(Showing showing);
    }
    
    [ExcludeFromCodeCoverage]
    public class ShowingContext : IShowingContext
    {
        private readonly ILunaCinemasDatabaseSettings _settings;
        private IMongoDatabase _database;
        private IMongoCollection<Showing> _showingsCollection;

        public ShowingContext(ILunaCinemasDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
            _showingsCollection = _database.GetCollection<Showing>(settings.ShowingsCollectionName);
        }

        public void AddShowing(Showing showing)
        {
            _showingsCollection.InsertOne(showing);
        }

        public void DeleteAll()
        {
            _showingsCollection.DeleteMany(showing => true);
        }

        public List<Showing> GetByFilmId(string filmId)
        {
            return _showingsCollection.Find(showing => showing.FilmId.Equals(filmId)).ToList();
        }

        public Showing GetById(string id)
        {
            List<Showing> resultFromDb = _showingsCollection.Find(showing => showing.Id.Equals(id)).ToList();
            return resultFromDb.Count > 0 ? resultFromDb[0] : null;
        }

        public bool UpdateShowing(Showing oldShowing, Showing newShowing)
        {
            try
            {
                _showingsCollection.ReplaceOne(showing => showing.Id.Equals(oldShowing.Id), newShowing);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}