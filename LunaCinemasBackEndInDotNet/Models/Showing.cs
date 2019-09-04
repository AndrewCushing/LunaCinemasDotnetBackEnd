using LunaCinemasBackEndInDotNet.BusinessLogic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Showing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("filmId")]
        public string FilmId { get; set; }
        [BsonElement("showingTime")]
        public string ShowingTime { get; set; }
        [BsonElement("date")]
        public string Date { get; set; }
        [BsonElement("seatsAvailable")]
        public int SeatsAvailable { get; set; }
        [BsonElement("totalNumberOfSeats")]
        public int TotalNumberOfSeats { get; set; }
        [BsonElement("seatAvailability")]
        public bool[][] SeatAvailability { get; set; }
        [BsonElement("screenType")]
        public string ScreenType { get; set; }
        [BsonElement("pricePerSeat")]
        public int PricePerSeat { get; set; }
        [BsonElement("_class")]
        public string JavaClass { get; set; }
    }
}