using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface ICommentContext
    {
        List<Comment> FindById(string reviewId);
        List<Comment> FindByReviewId(string reviewId);
        void AddComment(Comment comment);
    }
    
    public class CommentContext : ICommentContext
    {
        private readonly IMongoCollection<Comment> _commentCollection;
        public CommentContext(ILunaCinemasDatabaseSettings settings)
        {
            IMongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase db = client.GetDatabase(settings.DatabaseName);
            _commentCollection = db.GetCollection<Comment>(settings.CommentsCollectionName);
        }
        
        public List<Comment> FindById(string reviewId)
        {
            return _commentCollection.Find(comment => comment.ReviewId.Equals(reviewId)).ToList();
        }

        public List<Comment> FindByReviewId(string reviewId)
        {
            return _commentCollection.Find(comment => comment.ReviewId.Equals(reviewId)).ToList();
        }

        public void AddComment(Comment comment)
        {
            _commentCollection.InsertOne(comment);
        }
    }
}
