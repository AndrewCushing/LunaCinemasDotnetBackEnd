using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasTest.MockPersistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LunaCinemasTest
{
    [TestClass]
    public class MockedFilmTests
    {
        private static MockFilmContext _mockFilmContext;
        private static FilmController _testFilmController;

        [TestInitialize]
        public static void CreateMockFilmContext()
        {
            _mockFilmContext = new MockFilmContext();
            _testFilmController = new FilmController(new FilmGrabber(_mockFilmContext));
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

        [TestMethod]
        public void GetNewFilmsReturnsAllFilmsWhichAreReleased()
        {
            List<Film> expectedResult = new List<Film>();
            expectedResult.Add(new Film()
            {
                Title = "Test Film 1",
                IsReleased = true,
                BriefDescription = "Testing...",
                DetailedDescription = "Testing...",
                Actors = new []{"Testing... actor", "Testing... actor", "Testing... actor"},
                Directors = new []{"Testing... director", "Testing... director", "Testing... director"},
                Genres = new []{"Testing... genre", "Testing... genre", "Testing... genre"},
                Classification = "sldbflsdfbgvalkld",
                Id = "3954070936",
                ImagePath = "/asdf/asfd/asdf/adsf/",
                Length = "456",
                Year = "3",
                JavaClass = "222222"
            });
            expectedResult.Add(new Film()
            {
                Title = "Test Film 2",
                IsReleased = true,
                BriefDescription = "Testing...34",
                DetailedDescription = "Testing...",
                Actors = new []{"Testing... actorwer", "Testing... asfdfaactor", "Tesafsdfting... actor"},
                Directors = new []{"Testing... dirasdfector", "Testing... director", "Testasfding... director"},
                Genres = new []{"Testingasdf... genre", "Testsafding..asdf. genre", "Tsdfesting... gsaenre"},
                Classification = "sldbfdsaasdfflsdfbgvalkld",
                Id = "3954dssd070936",
                ImagePath = "/asdf/f/asfddf/adsf/",
                Length = "45f6",
                Year = "3d",
                JavaClass = "222fdf222"
            });
            _mockFilmContext.AddToGetReleasedFilmsResults(expectedResult);
            ActionResult<ResponseObject<Film>> actualResult = _testFilmController.GetNewFilms();
            Assert.AreEqual(true, actualResult.Value.successful);
            Assert.AreEqual(actualResult.Value.body, ResponseText.SuccessfullyRetrievedNewFilms);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i],actualResult.Value.contentList[i]);
            }
        }

        [TestMethod]
        public void GetUpcomingFilmsReturnsFilmsWhichAreNotReleased()
        {
            FilmController filmController = new FilmController(new FilmGrabber(_mockFilmContext));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetUpcomingFilms();
            Assert.AreEqual(true, actualResult.Value.successful);
            Assert.AreEqual(actualResult.Value.body, ResponseText.SuccessfullyRetrievedUpcomingFilms);
            foreach (Film film in actualResult.Value.contentList)
            {
                Assert.AreEqual(false, film.IsReleased);
            }
        }

        [TestMethod]
        public void GetFilmByIdReturnsExactly1Film()
        {
            FilmController filmController = new FilmController(new FilmGrabber(_mockFilmContext));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetFilm("5d650036280b7e2dc0b0d120");
            Assert.AreEqual(1, actualResult.Value.contentList.Count);
        }

        [TestMethod]
        public void SearchByFilmExactTitleGivesOnly1Match()
        {
            FilmController filmController = new FilmController(new FilmGrabber(_mockFilmContext));
            ActionResult<ResponseObject<Film>> actualResult = filmController.SearchFilms("Angel Has Fallen");
            Assert.AreEqual(1, actualResult.Value.contentList.Count);
            Assert.AreEqual("Angel Has Fallen", actualResult.Value.contentList[0].Title);
            Assert.AreEqual(false, actualResult.Value.contentList[0].IsReleased);
        }

        [TestMethod]

        public void ResponseIndicatesWhenDatabaseIsUnavailableAfterRequestingNewFilms()
        {
            FilmController filmController = new FilmController(new FilmGrabber(_mockFilmContext));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetNewFilms();
            Assert.AreEqual(false, actualResult.Value.successful);
            Assert.AreEqual(ResponseText.UnableToRetrieveNewFilms, actualResult.Value.body);
        }

        [TestMethod]
        public void ResponseIndicatesWhenDatabaseIsUnavailableAfterRequestingUpcomingFilms()
        {
            FilmController filmController = new FilmController(new FilmGrabber(_mockFilmContext));
            ActionResult<ResponseObject<Film>> actualResult = filmController.GetUpcomingFilms();
            Assert.AreEqual(false, actualResult.Value.successful);
            Assert.AreEqual(ResponseText.UnableToRetrieveUpcomingFilms, actualResult.Value.body);
        }
    }
}