using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockBookingContext : IBookingContext
    {
        public readonly List<Booking> Bookings = new List<Booking>();
        public bool AddBooking(Booking booking)
        {
            Bookings.Add(booking);
            return true;
        }

        public bool CancelBooking(Booking booking)
        {
            return Bookings.Remove(booking);
        }
    }
}