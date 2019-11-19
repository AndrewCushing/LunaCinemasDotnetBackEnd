using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string[] SeatsBooked { get; set; }
        public string ShowingId { get; set; }
        public DateTime TimeOfBooking { get; set; }
        public bool IsCancelled { get; set; }

        public Booking(string customerId, string[] seatsBooked, string showingId, DateTime timeOfBooking, bool isCancelled)
        {
            CustomerId = customerId;
            SeatsBooked = seatsBooked;
            ShowingId = showingId;
            TimeOfBooking = timeOfBooking;
            IsCancelled = isCancelled;
        }
    }
}