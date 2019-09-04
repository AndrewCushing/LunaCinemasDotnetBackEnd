using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int[][] Seats { get; set; }
        public string ShowingId { get; set; }
        public string EmailAddress { get; set; }
    }
}