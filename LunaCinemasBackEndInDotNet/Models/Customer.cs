using System.Collections.Generic;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class Customer : User
    {
        private readonly List<string> _bookingIds;
        public Customer(string firstName, string lastName, string email, string password)
            : base(firstName, lastName, email, password)
        {
            _bookingIds = new List<string>();
        }

        public void AddBooking(string bookingId)
        {
            _bookingIds.Add(bookingId);
        }

        public List<string> GetBookings()
        {
            return _bookingIds;
        }
    }
}