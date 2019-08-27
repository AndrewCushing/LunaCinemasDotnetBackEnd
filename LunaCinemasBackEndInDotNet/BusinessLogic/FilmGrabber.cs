using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class FilmGrabber
    {
        private readonly IMongoCollection<Film> _films;

        public FilmGrabber(ILunaCinemasDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _films = database.GetCollection<Film>(settings.FilmsCollectionName);
        }

        public ActionResult<List<Film>> Get()
        {
            var data = _films.Find(book => true).ToList();
            Console.WriteLine(data);
            Console.WriteLine(data.Count);
            return data;
        }
            

        public Film Get(string id) =>
            _films.Find<Film>(book => book.Id == id).FirstOrDefault();

        public Film Create(Film book)
        {
            _films.InsertOne(book);
            return book;
        }

        public void Update(string id, Film bookIn) =>
            _films.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Film bookIn) =>
            _films.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _films.DeleteOne(book => book.Id == id);

        //public ResponseObject<Film> GetAll()
        //{
        //    return new ResponseObject<Film>(true, "Got all the films", new List<Film>());
        //}

        //public ResponseObject<Film> Search(string searchText)
        //{
        //    return new ResponseObject<Film>(true, $"Got all the films using the search query {searchText}", new List<Film>());
        //}
    }
}