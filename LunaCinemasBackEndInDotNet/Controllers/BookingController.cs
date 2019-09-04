using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController
    {
        private BookingHandler _businessware;

        public BookingController(BookingHandler businessware)
        {
            _businessware = businessware;
        }

        [Route("booktickets/{showingId}")]
        [HttpPost("showingId")]
        public ActionResult<ResponseObject<Showing>> BookTickets(string showingId,
            [FromBody] string[] seatsToBook)
        {
            return _businessware.BookTickets(showingId, seatsToBook);
        }
    }
}