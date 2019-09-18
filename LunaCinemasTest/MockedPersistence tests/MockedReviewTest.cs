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
    public class MockedReviewTests
    {
        private static MockFilmContext _filmContext;
        private static MockReviewContext _reviewContext;

        [TestInitialize]
        public void CreateMockContexts()
        {
            _filmContext = new MockFilmContext();
            _reviewContext = new MockReviewContext();
        }
        
        [TestMethod]
        public void ReviewsCanBeSubmitted()
        {
            ReviewsController controller = new ReviewsController(new ReviewFilter(_reviewContext, _filmContext));
            _filmContext.AddFindByIdResult(new Film(){Id = "5d650036280b7e2dc0b0d121"});
            ActionResult<ResponseObject<object>> actualResponse = controller.SubmitReview("5d650036280b7e2dc0b0d121", "Test user", "3",
                "This is a test review. Nothing to see here.");
            Assert.AreEqual(true, actualResponse.Value.successful);
            Assert.AreEqual(1, _reviewContext.FindByFilmId("5d650036280b7e2dc0b0d121").Count);
            Assert.AreEqual("Test user", _reviewContext.FindByFilmId("5d650036280b7e2dc0b0d121")[0].Username);
        }

        [TestMethod]
        public void ReviewsCanBeRetrievedByFilmId()
        {
            _filmContext.AddFindByIdResult(new Film(){Id = "1234",Title = "The Matrix"});
            _reviewContext.AddReview(new Review(){FilmId = "1234", Username = "Jeff", ReviewBody = "Great film"});
            _reviewContext.AddReview(new Review(){FilmId = "1234", Username = "Bob", ReviewBody = "Best film ever!"});
            _reviewContext.AddReview(new Review(){FilmId = "1234", Username = "Sally", ReviewBody = "Legendary film"});
            _reviewContext.AddReview(new Review(){FilmId = "1234", Username = "Sarah", ReviewBody = "Awesome film"});
            ReviewsController controller = new ReviewsController(new ReviewFilter(_reviewContext, _filmContext));
            ActionResult<ResponseObject<object>> actualResponse =
                controller.GetReviewsByFilmId("1234");
            Assert.IsTrue(actualResponse.Value.successful);
            Assert.IsNotNull(actualResponse.Value.contentList);
            Assert.AreEqual(5, actualResponse.Value.contentList.Count);
        }

        [TestMethod]
        public void ProhibitedWordsInReviewsAreCensoredWhenReviewIsRetrieved()
        {
            ReviewsController controller = new ReviewsController(new ReviewFilter(_reviewContext, _filmContext));
            _filmContext.AddFindByIdResult(new Film(){Id = "5d650036280b7e2dc0b0d121"});
            ActionResult<ResponseObject<object>> actualResponse = controller.SubmitReview("5d650036280b7e2dc0b0d121", "Test user", "3",
                "This is a test review. natwesT");
            Assert.AreEqual(true, actualResponse.Value.successful);
            List<Review> reviews = _reviewContext.FindByFilmId("5d650036280b7e2dc0b0d121");
            Review testReview = reviews[0];
            Assert.AreEqual("This is a test review. *******", testReview.ReviewBody);
        }
    }
}