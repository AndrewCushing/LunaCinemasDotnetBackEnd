using LunaCinemasBackEndInDotNet.BusinessLogic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("filmId")]
        public string FilmId { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("rating")]
        public string Rating { get; set; }
        [BsonElement("review")]
        public string ReviewBody { get; set; }
        [BsonElement("_class")]
        public string JavaClass { get; set; }

        public Review(string filmId, string username, string rating, string reviewBody)
        {
            FilmId = filmId;
            Username = username;
            Rating = rating;
            ReviewBody = reviewBody;
        }
        
        public Review (){ }
    }
}