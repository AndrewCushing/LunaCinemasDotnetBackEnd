using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class AccessToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid Token { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryTime { get; set; }

        public static readonly int MinutesBeforeTokenExpiry = 5;

        public AccessToken(string userId)
        {
            UserId = userId;
            Token = Guid.NewGuid();
            ExpiryTime = DateTime.Now.AddMinutes(MinutesBeforeTokenExpiry);
        }
    }
}