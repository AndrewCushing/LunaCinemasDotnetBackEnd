using System;
using System.Diagnostics.CodeAnalysis;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IBookingContext
    {
        bool AddBooking(Booking booking);
        bool CancelBooking(Booking booking);
    }
    
    [ExcludeFromCodeCoverage]
    public class BookingContext : IBookingContext
    {
        private IMongoCollection<Booking> _bookingCollection;

        public BookingContext(ILunaCinemasDatabaseSettings settings)
        {
            _bookingCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<Booking>(settings.BookingCollectionName);
        }
        public bool AddBooking(Booking booking)
        {
            try
            {
                _bookingCollection.InsertOne(booking);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelBooking(Booking booking)
        {
            return true;
        }
    }
}