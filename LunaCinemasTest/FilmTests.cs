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

        private ILunaCinemasDatabaseSettings getIncorrectDatabaseSettings()
        {
            return new LunaCinemasDatabaseSettings()
            {
                FilmsCollectionName = "film",
                DatabaseName = "LunaCinemas",
                ConnectionString = "mongodb://localhost:27027"
            };
        }
        
        [Fact]
        public void GetNewFilmsReturnsOnlyFilmsWhichAreNotReleased()
        {
            FilmController filmController = new FilmController(new FilmGrabber(getTestSettings()));
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
            FilmController filmController = new FilmController(new FilmGrabber(getTestSettings()));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetUpcomingFilms();
            Assert.AreEqual(true, actualResult.Value.successful);
            Assert.AreEqual(actualResult.Value.body,ResponseText.SuccessfullyRetrievedUpcomingFilms);
            foreach(Film film in actualResult.Value.contentList)
            {
                Assert.AreEqual(false, film.IsReleased);
            }
        }

        [Fact]
        public void GetFilmByIdReturnsExactly1Film()
        {
            FilmController filmController = new FilmController(new FilmGrabber(getTestSettings()));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetFilm("5d650036280b7e2dc0b0d120");
            Assert.AreEqual(1,actualResult.Value.contentList.Count);
        }

        [Fact]
        public void SearchByFilmExactTitleGivesOnly1Match()
        {
            FilmController filmController = new FilmController(new FilmGrabber(getTestSettings()));
            ActionResult<ResponseObject<Film>> actualResult = filmController.SearchFilms("Angel Has Fallen");
            Assert.AreEqual(1, actualResult.Value.contentList.Count);
            Assert.AreEqual("Angel Has Fallen",actualResult.Value.contentList[0].Title);
            Assert.AreEqual(false, actualResult.Value.contentList[0].IsReleased);
        }

        [Fact]
        public void ResponseIndicatesWhenDatabaseIsUnavailableAfterRequestingNewFilms()
        {
            FilmController filmController = new FilmController(new FilmGrabber(getIncorrectDatabaseSettings()));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetNewFilms();
            Assert.AreEqual(false, actualResult.Value.successful);
            Assert.AreEqual(ResponseText.UnableToRetrieveNewFilms, actualResult.Value.body);
        }

        [Fact]
        public void ResponseIndicatesWhenDatabaseIsUnavailableAfterRequestingUpcomingFilms()
        {
            FilmController filmController = new FilmController(new FilmGrabber(getIncorrectDatabaseSettings()));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetUpcomingFilms();
            Assert.AreEqual(false, actualResult.Value.successful);
            Assert.AreEqual(ResponseText.UnableToRetrieveUpcomingFilms, actualResult.Value.body);
        }
    }
}