using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class CommentFilter : ContentFilter
    {
        private readonly ILunaCinemasDatabaseSettings _settings;
        private IMongoDatabase _database;

        public CommentFilter(ILunaCinemasDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }
        public ActionResult<ResponseObject<object>> GetComments(string reviewId)
        {
            IMongoCollection<Review> reviews = _database.GetCollection<Review>(_settings.ReviewsCollectionName);
            Review review = reviews.Find(reviewDoc => reviewDoc.Id.Equals(reviewId)).ToList()[0];
            IMongoCollection<Film> films = _database.GetCollection<Film>(_settings.FilmsCollectionName);
            Film film = films.Find(filmDoc => filmDoc.Id.Equals(review.FilmId)).ToList()[0];
            IMongoCollection<Comment> commentsCollection = _database.GetCollection<Comment>(_settings.CommentsCollectionName);
            List<Comment> commentsList = commentsCollection.Find(comment => true).ToList();
            ResponseObject<Object> res = new ResponseObject<object>(true, $"Retrieved {commentsList.Count} comments for that review.", new List<object>());
            res.contentList.Add(film);
            res.contentList.Add(review);
            res.contentList.AddRange(commentsList);
            return res;
        }

        public ActionResult<ResponseObject<Object>> AddComment(string reviewId, string username, string commentBody)
        {
            IMongoCollection<Comment> comments = _database.GetCollection<Comment>(_settings.CommentsCollectionName);
            comments.InsertOne(new Comment(reviewId, username, commentBody));
            return GetComments(reviewId);
        }
    }
}