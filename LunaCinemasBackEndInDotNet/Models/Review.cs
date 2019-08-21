using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FilmId { get; set; }
        public string UserName { get; set; }
        public string Rating { get; set; }
        public string ReviewBody { get; set; }
    }
}