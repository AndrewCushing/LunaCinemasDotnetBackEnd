using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class BookingHandler
    {
        private readonly ILunaCinemasDatabaseSettings _settings;
        private IMongoDatabase _database;
        private ShowingHandler _showingHandler;

        public BookingHandler(ILunaCinemasDatabaseSettings settings, ShowingHandler showingHandler)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
            _showingHandler = showingHandler;
        }

        public ActionResult<ResponseObject<Showing>> BookTickets(string showingId, string[] seatsToBook)
        {
            return _showingHandler.AttemptBooking(showingId, seatsToBook);
        }
    }
}