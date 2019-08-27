using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ReviewId { get; set; }
        public string Username { get; set; }
        public string body { get; set; }
    }
}