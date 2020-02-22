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
        public void CreateMockFilmContext()
        {
            _mockFilmContext = new MockFilmContext();
            _testFilmController = new FilmController(new FilmService(_mockFilmContext));
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
            List<Film> expectedResult = new List<Film>();
            expectedResult.Add(new Film()
            {
                Title = "Test Film 3",
                IsReleased = false,
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
                Title = "Test Film 4",
                IsReleased = false,
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
            _mockFilmContext.AddToGetUpcomingFilmsResults(expectedResult);
            ActionResult<ResponseObject<Film>> actualResult = _testFilmController.GetUpcomingFilms();
            Assert.AreEqual(true, actualResult.Value.successful);
            Assert.AreEqual(actualResult.Value.body, ResponseText.SuccessfullyRetrievedUpcomingFilms);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i],actualResult.Value.contentList[i]);
            }
        }

        [TestMethod]
        public void GetFilmByIdReturnsExactly1Film()
        {
            string expectedFilmId = "23652165435866168165431683513146846845132askfbhasjlhdcliaueh948robtfoe4btk3jfsk0";
            Film expectedFilm = new Film(){Id = expectedFilmId, Title = "Right"};
            _mockFilmContext.AddFindByIdResult(expectedFilm);
            _mockFilmContext.AddFindByIdResult(new Film(){Id = "incorrect film to retrieve", Title = "Wrong"});
            _mockFilmContext.AddFindByIdResult(new Film(){Id = "lskfnlasiubcieub", Title = "Wrong"});
            _mockFilmContext.AddFindByIdResult(new Film(){Id = "ccdddddddddddddddddddddddddddddddddddddddd", Title = "Wrong"});
            ActionResult<ResponseObject<Film>> actualResult = _testFilmController.GetFilm(expectedFilmId);
            Assert.AreEqual(1, actualResult.Value.contentList.Count);
            Assert.AreEqual(expectedFilmId, actualResult.Value.contentList[0].Id);
        }

        [TestMethod]
        public void SearchUsingExactFilmExactTitleToGetOnly1Match()
        {
            List<Film> dummyFilmsList = new List<Film>();
            dummyFilmsList.Add(new Film(){Id = "jeff", Title = "kljfnalkdjf", Directors = new []{"hi"}, Genres = new []{"skfndlsak"}, Actors = new []{"Nonsense"}});
            dummyFilmsList.Add(new Film(){Id = "njkjn", Title = "Angel Has Fallen", Directors = new []{"hi"}, Genres = new []{"skfndlsak"}, Actors = new []{"Nonsense"}});
            dummyFilmsList.Add(new Film(){Id = "dcdcd", Title = "Angel Had not Fallen", Directors = new []{"hi"}, Genres = new []{"skfndlsak"}, Actors = new []{"Nonsense"}});
            dummyFilmsList.Add(new Film(){Id = "kdkd", Title = "Angel fell", Directors = new []{"hi"}, Genres = new []{"skfndlsak"}, Actors = new []{"Nonsense"}});
            dummyFilmsList.Add(new Film(){Id = "sdcc", Title = "Angels are dumb", Directors = new []{"hi"}, Genres = new []{"skfndlsak"}, Actors = new []{"Nonsense"}});
            _mockFilmContext.AddToGetAllFilmResults(dummyFilmsList);
            ActionResult<ResponseObject<Film>> actualResult = _testFilmController.SearchFilms("Angel Has Fallen");
            Assert.AreEqual(1, actualResult.Value.contentList.Count);
            Assert.AreEqual("Angel Has Fallen", actualResult.Value.contentList[0].Title);
            Assert.AreEqual("njkjn", actualResult.Value.contentList[0].Id);
        }

        [TestMethod]
        public void AttemptingToRetrieveUpcomingFilmsWhenDbIsDownGivesAppropriateResponse()
        {
            _mockFilmContext.BreakPersistence();
            ActionResult<ResponseObject<Film>> actualResponse = _testFilmController.GetUpcomingFilms();
            Assert.IsFalse(actualResponse.Value.successful);
        }

        [TestMethod]
        public void AttemptingToRetrieveNewFilmsWhenDbIsDownGivesAppropriateResponse()
        {
            _mockFilmContext.BreakPersistence();
            ActionResult<ResponseObject<Film>> actualResponse = _testFilmController.GetNewFilms();
            Assert.IsFalse(actualResponse.Value.successful);
        }
    }
}