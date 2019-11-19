using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ReviewFilter : ContentFilter
    {
        private readonly IReviewContext _reviewContext;
        private readonly IFilmContext _filmContext;

        public ReviewFilter(IReviewContext reviewContext, IFilmContext filmContext)
        {
            _reviewContext = reviewContext;
            _filmContext = filmContext;
        }

        public ActionResult<ResponseObject<object>> GetByFilmId(string id)
        {
            try
            {
                List<Film> film = _filmContext.FindById(id);
                List<Review> reviews = _reviewContext.FindByFilmId(id);
                ResponseObject<object> res = new ResponseObject<object>(true, $"Retrieved film data and reviews as a list. There was {reviews.Count} reviews for that film", new List<object>(film));
                if (reviews.Count > 0)
                {
                    res.contentList.AddRange(reviews);
                }
                return res;
            }
            catch (Exception)
            {
                return new ResponseObject<object>(false, $"That film ID doesn't match any films in the database", null);
            }
        }

        public ActionResult<ResponseObject<object>> AddReview(Review review)
        {
            _reviewContext.AddReview(review);
            return GetByFilmId(review.FilmId);
        }

        internal void DeleteAll()
        {
            _reviewContext.deleteAll();
        }
    }
}