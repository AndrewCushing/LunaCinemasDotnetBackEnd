using LunaCinemasBackEndInDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Xunit.Sdk;

namespace LunaCinemasTest
{
    [TestClass]
    public class BookingAndShowingTest
    {
        private ILunaCinemasDatabaseSettings GetCorrectSettings()
        {
            return new LunaCinemasDatabaseSettings()
            {
                FilmsCollectionName = "film",
                ReviewsCollectionName = "review",
                CommentsCollectionName = "comment",
                ShowingsCollectionName = "showing",
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "LunaCinemas"
            };
        }
        
        [Fact]
        public void ShowingInformationCanBeRetrieved()
        {

        }
    }
}