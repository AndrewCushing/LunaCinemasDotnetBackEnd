using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController
    {
        private BookingService _businessware;

        public BookingController(BookingService businessware)
        {
            _businessware = businessware;
        }

        [Route("booktickets/{showingId}")]
        [HttpPost("showingId")]
        public ActionResult<ResponseObject<Showing>> BookTickets(string showingId, [FromBody] string[] seatsToBook)
        {
            return _businessware.BookTickets(showingId, seatsToBook);
        }
    }
}