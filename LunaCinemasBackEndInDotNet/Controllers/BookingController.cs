using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly BookingService _businessware;

        public BookingController(BookingService businessware)
        {
            _businessware = businessware;
        }

        [Route("booktickets/{showingId}")]
        [HttpPost("showingId")]
        public ActionResult<ResponseObject<Showing>> BookTickets(string showingId, [FromBody] string[] seatsToBook)
        {
            string id = "lwfjweij";
            

            return _businessware.BookTickets(showingId, seatsToBook);
        }
    }
}