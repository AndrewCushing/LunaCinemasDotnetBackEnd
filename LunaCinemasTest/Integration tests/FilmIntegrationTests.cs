using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunaCinemasTest.Integration_tests
{
    [TestClass]
    public class FilmIntegrationTests
    {
        [TestMethod]
        public void ResponseIndicatesWhenDatabaseIsUnavailableAfterRequestingNewFilms()
        {
            FilmController filmController = new FilmController(new FilmGrabber(new FilmContext(IntegrationSettings.GetIncorrectSettings())));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetNewFilms();
            Assert.AreEqual(false, actualResult.Value.successful);
            Assert.AreEqual(ResponseText.UnableToRetrieveNewFilms, actualResult.Value.body);
        }

        [TestMethod]
        public void ResponseIndicatesWhenDatabaseIsUnavailableAfterRequestingUpcomingFilms()
        {
            FilmController filmController = new FilmController(new FilmGrabber(new FilmContext(IntegrationSettings.GetIncorrectSettings())));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetUpcomingFilms();
            Assert.AreEqual(false, actualResult.Value.successful);
            Assert.AreEqual(ResponseText.UnableToRetrieveUpcomingFilms, actualResult.Value.body);
        }
    }
}