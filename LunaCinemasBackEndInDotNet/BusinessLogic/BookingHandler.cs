using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class BookingHandler
    {
        private ShowingHandler _showingHandler;

        public BookingHandler(ShowingHandler showingHandler)
        {
            _showingHandler = showingHandler;
        }

        public ActionResult<ResponseObject<Showing>> BookTickets(string showingId, string[] seatsToBook)
        {
            return _showingHandler.AttemptBooking(showingId, seatsToBook);
        }
    }
}