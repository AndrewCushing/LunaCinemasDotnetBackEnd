using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ShowingHandler
    {
        private readonly ILunaCinemasDatabaseSettings _settings;
        private IMongoDatabase _database;

        public ShowingHandler(ILunaCinemasDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }
        
        public ActionResult<ResponseObject<object>> GetShowingsByFilmId(string filmId)
        {
            IMongoCollection<Showing> showingsCollection = _database.GetCollection<Showing>(_settings.ShowingsCollectionName);
            List<Showing> showingsForThisFilm =
                showingsCollection.Find(showing => showing.FilmId.Equals(filmId)).ToList();
            FilmGrabber filmGrabber = new FilmGrabber(_settings);
            Film filmObject = filmGrabber.GrabFilmObject(filmId);
            if (showingsForThisFilm.Count > 0)
            {
                ResponseObject<object> positiveResponse = new ResponseObject<object>(true,$"Retrieved {showingsForThisFilm.Count} showings for that filmId", new List<object>());
                positiveResponse.contentList.Add(filmObject);
                positiveResponse.contentList.AddRange(showingsForThisFilm);
                return positiveResponse;
            }
            ResponseObject<object> negativeResponse = new ResponseObject<object>(false,"No showings found for that film", new List<object>());
            negativeResponse.contentList.Add(filmObject);
            return negativeResponse;
        }

        public ActionResult<ResponseObject<Showing>> GetShowingById(string id)
        {
            Showing showingToReturn = getShowing(id);
            List<Showing> showingAsList = new List<Showing>();
            showingAsList.Add(showingToReturn);
            return showingToReturn != null ? 
                new ResponseObject<Showing>(true, "Showing retrieved", showingAsList) : 
                new ResponseObject<Showing>(false,"No showing found with that Id",null);
        }

        public ActionResult<ResponseObject<Showing>> AttemptBooking(string showingId, string[] seatsToBook)
        {
            Showing showing = getShowing(showingId);
            foreach (string seat in seatsToBook)
            {
                int[] coords = getSeatCoordsAsInts(seat);
                if (showing.SeatAvailability[coords[0]][coords[1]])
                {
                    List<Showing> showingAsList = new List<Showing>();
                    showingAsList.Add(showing);
                    return new ResponseObject<Showing>(false,"Unable to proceed with booking. Some of those seats have already been booked.", showingAsList);
                }
            }
            IMongoCollection<Showing> showingsCollection = _database.GetCollection<Showing>(_settings.ShowingsCollectionName);
            FilterDefinition<Showing> filterDefinition = new BsonDocumentFilterDefinition<Showing>(showing.ToBsonDocument());
            Showing newShowing = copyShowing(showing, seatsToBook);
            showingsCollection.ReplaceOne(filterDefinition, newShowing);
            return new ResponseObject<Showing>(true, "Your seats have been booked",null);
        }

        private Showing getShowing(string id)
        {
            IMongoCollection<Showing> showings = _database.GetCollection<Showing>(_settings.ShowingsCollectionName);
            return showings.Find(showing => showing.Id.Equals(id)).ToList()[0];
        }

        private bool[][] getNewSeatAvailability(bool[][] seatAvailability, string[] seatsToBook)
        {
            foreach (string seat in seatsToBook)
            {
                int[] coords = getSeatCoordsAsInts(seat);
                seatAvailability[coords[0]][coords[1]] = true;
            }
            return seatAvailability;
        }

        private int[] getSeatCoordsAsInts(string seatCoords)
        {
            int[] result = new int[2];
            string[] seatAsStrings = seatCoords.Split(":");
            result[0] = int.Parse(seatAsStrings[0]);
            result[1] = int.Parse(seatAsStrings[1]);
            return result;
        }

        private BsonDocument createShowingReplacement(Showing oldShowing, bool[][] newSeatAvailability)
        {
            BsonDocument result = new BsonDocument();
            result.Add("SeatAvailability", new BsonArray(newSeatAvailability));
            result.Add("Date", oldShowing.Date);
            result.Add("filmId", oldShowing.FilmId);
            result.Add("_class", oldShowing.JavaClass);
            result.Add("pricePerSeat", oldShowing.PricePerSeat);
            result.Add("screenType", oldShowing.ScreenType);
            result.Add("showingTime", oldShowing.ShowingTime);
            result.Add("totalNumberOfSeats", oldShowing.TotalNumberOfSeats);
            return result;
        }

        private Showing copyShowing(Showing oldShowing, string[] seatsToBook)
        {
            Showing result = new Showing();
            result.Id = oldShowing.Id;
            result.SeatAvailability = getNewSeatAvailability(oldShowing.SeatAvailability, seatsToBook);
            result.SeatsAvailable = oldShowing.SeatsAvailable - seatsToBook.Length;
            result.Date = oldShowing.Date;
            result.FilmId = oldShowing.FilmId;
            result.JavaClass = oldShowing.JavaClass;
            result.PricePerSeat = oldShowing.PricePerSeat;
            result.ScreenType = oldShowing.ScreenType;
            result.ShowingTime = oldShowing.ShowingTime;
            result.TotalNumberOfSeats = oldShowing.TotalNumberOfSeats;
            return result;
        }
    }
}