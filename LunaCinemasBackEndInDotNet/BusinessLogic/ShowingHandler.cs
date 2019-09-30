using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ShowingHandler
    {
        private readonly IShowingContext _dbContext;
        private readonly IFilmContext _filmContext;

        public ShowingHandler(IShowingContext dbContext, IFilmContext filmContext)
        {
            _dbContext = dbContext;
            _filmContext = filmContext;
        }
        
        public ActionResult<ResponseObject<object>> GetShowingsByFilmId(string filmId)
        {
            List<Showing> showingsForThisFilm = _dbContext.GetByFilmId(filmId);
            List<Film> filmAsList = _filmContext.FindById(filmId);
            if (showingsForThisFilm.Count > 0)
            {
                ResponseObject<object> positiveResponse = new ResponseObject<object>(true,$"Retrieved {showingsForThisFilm.Count} showings for that filmId", new List<object>(filmAsList));
                positiveResponse.contentList.AddRange(showingsForThisFilm);
                return positiveResponse;
            }
            ResponseObject<object> negativeResponse = new ResponseObject<object>(false,ResponseText.NoShowingsForThatFilm, new List<object>(filmAsList));
            return negativeResponse;
        }

        public ActionResult<ResponseObject<Showing>> AttemptBooking(string showingId, string[] seatsToBook)
        {
            Showing showing = _dbContext.GetById(showingId);
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
            Showing newShowing = createShowingWithSeatsToBook(showing, seatsToBook);
            bool success = _dbContext.UpdateShowing(showing, newShowing);
            return success
                ? new ResponseObject<Showing>(true, "Your seats have been booked", null)
                : new ResponseObject<Showing>(false, "Unable to book seats due to a problem with the database connection. Please try again later", null);
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

        private Showing createShowingWithSeatsToBook(Showing oldShowing, string[] seatsToBook)
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

        internal void AddShowing(string filmId, string screenType, string time, string date)
        {
            _dbContext.AddShowing(new Showing()
            {
                FilmId = filmId,
                Date = date,
                ShowingTime = time,
                ScreenType = screenType,
                PricePerSeat = screenType.Equals("Standard") ? 8 : 15,
                SeatsAvailable = screenType.Equals("Standard") ? 72 : 20,
                TotalNumberOfSeats = screenType.Equals("Standard") ? 72 : 20,
                SeatAvailability = getSeatAvailability("Standard")
            });
        }

        private bool[][] getSeatAvailability(string screenType)
        {
            if (screenType.Equals("Dbox"))
            {
                bool[][] temp = new bool[4][];
                for (int i = 0 ; i < 4 ; i++)
                {
                    temp[i] = new bool[5];
                }
                return temp;
            }
            else
            {
                bool[][] temp = new bool[10][];
                for (int i = 0; i < 10 ; i++)
                {
                    if (i < 2)
                    {
                        temp [i] = new bool[5];
                    } else if (i > 7)
                    {
                        temp[i] = new bool[10];
                    }
                    else
                    {
                        temp[i] = new bool[7];
                    }
                }
                return temp;
            }
        }

        internal void DeleteAll()
        {
            _dbContext.DeleteAll();
        }
    }
}