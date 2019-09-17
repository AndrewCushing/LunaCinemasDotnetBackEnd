using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Xunit;
using Xunit.Sdk;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LunaCinemasTest
{
    [TestClass]
    public class ReviewTest
    {
        private ILunaCinemasDatabaseSettings GetCorrectDBSettings()
        {
            return new LunaCinemasDatabaseSettings()
            {
                ConnectionString = "mongodb://localhost:27017",
                ReviewsCollectionName = "review",
                DatabaseName = "LunaCinemas",
                FilmsCollectionName = "film"
            };
        }
        
        [Fact]
        public void ReviewsCanBeSubmitted()
        {
            //ReviewsController controller = new ReviewsController(new ReviewFilter(GetCorrectDBSettings()));
            //ActionResult<ResponseObject<object>> actualResponse = controller.SubmitReview("5d650036280b7e2dc0b0d121", "Test user", "3",
            //    "This is a test review. Nothing to see here.");
            //Assert.AreEqual(true, actualResponse.Value.successful);
        }

        [Fact]
        public void ReviewsCanBeRetrievedByFilmId()
        {
            //ReviewsController controller = new ReviewsController(new ReviewFilter(GetCorrectDBSettings()));
            //ActionResult<ResponseObject<object>> actualResponse =
            //    controller.GetReviewsByFilmId("5d650036280b7e2dc0b0d121");
            //Assert.IsTrue(actualResponse.Value.successful);
            //Assert.IsNotNull(actualResponse.Value.contentList);
        }

        [Fact]
        public void ProhibitedWordsInReviewsAreCensored()
        {
            //ILunaCinemasDatabaseSettings settings = GetCorrectDBSettings();
            //ReviewsController controller = new ReviewsController(new ReviewFilter(settings));
            //ActionResult<ResponseObject<object>> actualResponse = controller.SubmitReview("5d650036280b7e2dc0b0d121", "Test user", "3",
            //    "This is a test review. natwesT");
            //Assert.AreEqual(true, actualResponse.Value.successful);
            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);
            //var reviewCollection = database.GetCollection<Review>(settings.ReviewsCollectionName);
            //List<Review> reviews = reviewCollection.Find(review => true).ToList();
            //Review testReview = reviews[reviews.Count - 1];
            //Assert.AreEqual("This is a test review. *******",testReview.ReviewBody);
        }
    }
}