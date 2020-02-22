using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface ICommentContext
    {
        List<Comment> FindById(string id);
        List<Comment> FindByReviewId(string reviewId);
        void AddComment(Comment comment);
        void DeleteAll();
    }
    
    [ExcludeFromCodeCoverage]
    public class CommentContext : ICommentContext
    {
        private readonly IMongoCollection<Comment> _commentCollection;
        public CommentContext(ILunaCinemasDatabaseSettings settings)
        {
            IMongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase db = client.GetDatabase(settings.DatabaseName);
            _commentCollection = db.GetCollection<Comment>(settings.CommentsCollectionName);
        }
        
        public List<Comment> FindById(string id)
        {
            return _commentCollection.Find(comment => comment.Id.Equals(id)).ToList();
        }

        public List<Comment> FindByReviewId(string reviewId)
        {
            return _commentCollection.Find(comment => comment.ReviewId.Equals(reviewId)).ToList();
        }

        public void AddComment(Comment comment)
        {
            _commentCollection.InsertOne(comment);
        }

        public void DeleteAll()
        {
            _commentCollection.DeleteMany(comment => true);
        }
    }
}
