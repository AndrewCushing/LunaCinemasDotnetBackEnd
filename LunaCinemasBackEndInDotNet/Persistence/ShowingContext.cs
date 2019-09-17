using System;
using System.Collections.Generic;
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
    }
    
    public class ShowingContext : IShowingContext
    {
        private readonly ILunaCinemasDatabaseSettings _settings;
        private IMongoDatabase _database;

        public ShowingContext(ILunaCinemasDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }

        public List<Showing> GetByFilmId(string filmId)
        {
            IMongoCollection<Showing> showingsCollection = _database.GetCollection<Showing>(_settings.ShowingsCollectionName);
            return showingsCollection.Find(showing => showing.FilmId.Equals(filmId)).ToList();
        }

        public Showing GetById(string id)
        {
            IMongoCollection<Showing> showingsCollection = _database.GetCollection<Showing>(_settings.ShowingsCollectionName);
            List<Showing> resultFromDb = showingsCollection.Find(showing => showing.Id.Equals(id)).ToList();
            return resultFromDb.Count > 0 ? resultFromDb[0] : null;
        }

        public bool UpdateShowing(Showing oldShowing, Showing newShowing)
        {
            try
            {
                _database.GetCollection<Showing>(_settings.ShowingsCollectionName)
                    .ReplaceOne(new BsonDocumentFilterDefinition<Showing>(oldShowing.ToBsonDocument()), newShowing);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}