using System.Collections.Generic;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Customer : User
    {
        public List<string> BookingIds { get; set; }
        public Customer(string firstName, string lastName, string email, string password)
            : base(firstName, lastName, email, password)
        {
            BookingIds = new List<string>();
        }

        public void AddBooking(string bookingId)
        {
            BookingIds.Add(bookingId);
        }

        public List<string> GetBookings()
        {
            return BookingIds;
        }
    }
}