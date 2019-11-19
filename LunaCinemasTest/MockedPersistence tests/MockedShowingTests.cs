using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasTest.MockPersistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunaCinemasTest
{
    [TestClass]
    public class MockedShowingTests
    {
        private MockShowingContext _showingContext;
        private ShowingsController _showingsController;
        private MockFilmContext _filmContext;

        [TestInitialize]
        public void CreateMockedController()
        {
            _showingContext = new MockShowingContext();
            _filmContext = new MockFilmContext();
            _showingsController = new ShowingsController(new ShowingService(_showingContext, _filmContext));
        }

        [TestMethod]
        public void ShowingsCanBeRetrievedByFilmIdWithShowingsPresentInDb()
        {
            _filmContext.AddFindByIdResult(new Film(){Id = "hitmeharder"});
            _showingContext.AddShowing(new Showing(){FilmId = "hitmeharder", PricePerSeat = 215469523});
            ActionResult<ResponseObject<object>> actualResult = _showingsController.GetShowingsByFilmId("hitmeharder");
            Assert.IsTrue(actualResult.Value.successful);
            Assert.AreEqual("hitmeharder",((Showing)actualResult.Value.contentList[1]).FilmId);
            Assert.AreEqual(215469523,((Showing)actualResult.Value.contentList[1]).PricePerSeat);
        }

        [TestMethod]
        public void AttemptingToGetShowingsWhenThereAreNoneGivesAppropriateResponse()
        {
            _filmContext.AddFindByIdResult(new Film(){Id = "hitmeharder"});
            ActionResult<ResponseObject<object>> actualResult = _showingsController.GetShowingsByFilmId("hitmeharder");
            Assert.IsFalse(actualResult.Value.successful);
            Assert.AreEqual(ResponseText.NoShowingsForThatFilm, actualResult.Value.body);
        }
    }
}