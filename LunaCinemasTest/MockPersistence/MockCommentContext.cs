using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockCommentContext : ICommentContext
    {
        private readonly List<Comment> _comments = new List<Comment>();
        public List<Comment> FindById(string id)
        {
            return _comments.FindAll(comment => comment.ReviewId.Equals(id));
        }

        public List<Comment> FindByReviewId(string reviewId)
        {
            return _comments.FindAll(comment => comment.ReviewId.Equals(reviewId));
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }
    }
}