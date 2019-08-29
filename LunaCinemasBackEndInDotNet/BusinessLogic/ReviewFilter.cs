using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ReviewFilter : ContentFilter
    {
        private readonly ILunaCinemasDatabaseSettings _settings;
        private IMongoDatabase _database;
        private IMongoCollection<Review> _reviews;

        public ReviewFilter(ILunaCinemasDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }
        private void UpdateReviews()
        {
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
            _reviews = _database.GetCollection<Review>(_settings.ReviewsCollectionName);
        }

        public ActionResult<ResponseObject<Object>> GetByFilmId(string id)
        {
            UpdateReviews();
            Film film = _database.GetCollection<Film>(_settings.FilmsCollectionName).Find(filmDoc => filmDoc.Id.Equals(id))
                .ToList()[0];
            List<Review> reviews = _reviews.Find(review => review.FilmId.Equals(id)).ToList();
            ResponseObject<Object> res = new ResponseObject<object>(true, $"Retrieved film data and reviews as a list. There was {reviews.Count} reviews for that film",new List<Object>());
            res.contentList.Add(film);
            if (reviews.Count > 0)
            {
                res.contentList.AddRange(reviews);
            }
            return res;
        }

        public ActionResult<ResponseObject<Object>> AddReview(string filmId, string username, string rating, string reviewBody)
        {
            UpdateReviews();
            Review reviewToInsert = new Review();
            reviewToInsert.FilmId = filmId;
            reviewToInsert.Username = filterStuff(username);
            reviewToInsert.Rating = rating;
            reviewToInsert.ReviewBody = filterStuff(reviewBody);
            _reviews.InsertOne(reviewToInsert);
            return GetByFilmId(filmId);
        }
    }
}