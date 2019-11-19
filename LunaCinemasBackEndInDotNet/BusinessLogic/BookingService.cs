using System;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class BookingService
    {
        private ShowingService _showingService;
        private IBookingContext _bookingContext;

        public BookingService(ShowingService showingService, IBookingContext bookingContext)
        {
            _showingService = showingService;
            _bookingContext = bookingContext;
        }

        public ActionResult<ResponseObject<Showing>> BookTickets(string showingId, string[] seatsToBook)
        {
            ActionResult<ResponseObject<Showing>> bookingResult = _showingService.AttemptBooking(showingId, seatsToBook);
            if (bookingResult.Value.successful)
            {
                _bookingContext.AddBooking(new Booking("---", seatsToBook, showingId, DateTime.Now, false));
            }
            return bookingResult;
        }
    }
}