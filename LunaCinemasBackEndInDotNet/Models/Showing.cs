using LunaCinemasBackEndInDotNet.BusinessLogic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Showing : IResponseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FilmId { get; set; }
        public string ShowingTime { get; set; }
        public string Date { get; set; }
        public int SeatsAvailable { get; set; }
        public int TotalNumberOfSeats { get; set; }
        public bool[][] SeatAvailability { get; set; }
        public string ScreenType { get; set; }
        public int PricePerSeat { get; set; }
    }
}