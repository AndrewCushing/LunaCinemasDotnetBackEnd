using System.Collections.Generic;
using System.Web.Http.Filters;
using System.Web.Mvc;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LunaCinemasTest
{
    [TestClass]
    public class FilmTest
    {
        private ILunaCinemasDatabaseSettings getTestSettings()
        {
            return new LunaCinemasDatabaseSettings()
            {
                FilmsCollectionName = "film",
                DatabaseName = "LunaCinemas",
                ConnectionString = "mongodb://localhost:27017"
            };
        }
        
        [Fact]
        public void GetNewFilmsReturnsOnlyFilmsWhichAreNotReleased()
        {
            ILunaCinemasDatabaseSettings settings = getTestSettings();
            FilmController filmController = new FilmController(new FilmGrabber(settings));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetNewFilms();
            Assert.AreEqual(true, actualResult.Value.successful);
            Assert.AreEqual(actualResult.Value.body,ResponseText.SuccessfullyRetrievedNewFilms);
            foreach(Film film in actualResult.Value.contentList)
            {
                Assert.AreEqual(true, film.IsReleased);
            }
        }

        [Fact]
        public void GetUpcomingFilmsReturnsFilmsWhichAreNotReleased()
        {
            ILunaCinemasDatabaseSettings settings = getTestSettings();
            FilmController filmController = new FilmController(new FilmGrabber(settings));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetUpcomingFilms();
            Assert.AreEqual(true, actualResult.Value.successful);
            Assert.AreEqual(actualResult.Value.body,ResponseText.SuccessfullyRetrievedUpcomingFilms);
            foreach(Film film in actualResult.Value.contentList)
            {
                Assert.AreEqual(false, film.IsReleased);
            }
        }
    }
}
