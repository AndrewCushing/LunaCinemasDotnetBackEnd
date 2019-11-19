using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class CommentFilter : ContentFilter
    {
        private readonly ICommentContext _commentContext;
        private readonly IReviewContext _reviewContext;
        private readonly IFilmContext _filmContext;

        public CommentFilter(ICommentContext commentContext, IReviewContext reviewContext, IFilmContext filmContext)
        {
            _commentContext = commentContext;
            _reviewContext = reviewContext;
            _filmContext = filmContext;
        }
        public ActionResult<ResponseObject<object>> GetComments(string reviewId)
        {
            Review review = _reviewContext.FindById(reviewId)?[0];
            Film film = _filmContext.FindById(review?.FilmId)?[0];
            if (review == null || film == null)
            {
                return new ResponseObject<object>(false, "Invalid review ID", null);
            }
            List<Comment> commentsList = _commentContext.FindByReviewId(reviewId);
            ResponseObject<object> res = new ResponseObject<object>(true, $"Retrieved {commentsList.Count} comments for that review.", new List<object>());
            res.contentList.Add(film);
            res.contentList.Add(review);
            res.contentList.AddRange(commentsList);
            return res;
        }

        public ActionResult<ResponseObject<object>> AddComment(Comment comment)
        {
            _commentContext.AddComment(comment);
            return GetComments(comment.ReviewId);
        }

        internal void deleteAll()
        {
            _commentContext.DeleteAll();
        }
    }
}