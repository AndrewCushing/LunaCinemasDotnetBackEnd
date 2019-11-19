using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasTest.MockPersistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunaCinemasTest
{
    [TestClass]
    public class MockedBookingTests
    {
        private MockShowingContext _mockShowingContext;
        private MockFilmContext _mockFilmContext;
        private BookingController _bookingController;
            
        [TestInitialize]
        public void CreateMocks()
        {
            _mockFilmContext = new MockFilmContext();
            _mockShowingContext = new MockShowingContext();
            _bookingController = new BookingController(new BookingService(new ShowingService(_mockShowingContext,_mockFilmContext)));
        }

        [TestMethod]
        public void BookingASeatWhichIsNotTakenCorrectlyUpdatesShowing()
        {
            _mockShowingContext.AddShowing(new Showing()
            {
                Id = "1234",
                FilmId = "2222222",
                Date = "12/13/14",
                JavaClass = "Bob",
                PricePerSeat = 3,
                ScreenType = "Standard",
                SeatsAvailable = 70,
                ShowingTime = "15:00",
                TotalNumberOfSeats = 70,
                SeatAvailability = new[]
                {
                    new[] {false, false, false},
                    new[] {false, false, false},
                    new[] {false, false, false}
                }
            });
            ActionResult<ResponseObject<Showing>> actualResult = _bookingController.BookTickets("1234", new[] {"1:1"});
            Assert.IsTrue(actualResult.Value.successful);
            Assert.IsTrue(_mockShowingContext.GetById("1234").SeatAvailability[1][1]);
            Assert.IsFalse(_mockShowingContext.GetById("1234").SeatAvailability[0][1]);
            Assert.IsFalse(_mockShowingContext.GetById("1234").SeatAvailability[1][0]);
        }

        [TestMethod]
        public void AttemptingToBookASeatWhichIsTakenWillBeUnsuccessful()
        {
            _mockShowingContext.AddShowing(new Showing()
            {
                Id = "1234",
                FilmId = "2222222",
                Date = "12/13/14",
                JavaClass = "Bob",
                PricePerSeat = 3,
                ScreenType = "Standard",
                SeatsAvailable = 70,
                ShowingTime = "15:00",
                TotalNumberOfSeats = 70,
                SeatAvailability = new[]
                {
                    new[] {false, false, false},
                    new[] {false, true, false},
                    new[] {false, false, false}
                }
            });
            ActionResult<ResponseObject<Showing>> actualResult = _bookingController.BookTickets("1234", new[] {"1:1"});
            Assert.IsFalse(actualResult.Value.successful);
            bool[][] expectedSeats = new[]
            {
                new[] {false, false, false},
                new[] {false, true, false},
                new[] {false, false, false}
            };
            AssertSeatsAreEqual(actualResult.Value.contentList[0].SeatAvailability, _mockShowingContext.GetById("1234").SeatAvailability);
            AssertSeatsAreEqual(expectedSeats, _mockShowingContext.GetById("1234").SeatAvailability);
        }

        private void AssertSeatsAreEqual(bool[][] expectedSeats, bool[][] actualSeats)
        {
            for (int i = 0; i < expectedSeats.Length; i++)
            {
                for (int j = 0; j < expectedSeats[i].Length; j++)
                {
                    Assert.AreEqual(expectedSeats[i][j],actualSeats[i][j]);
                }
            }
        }
    }
}