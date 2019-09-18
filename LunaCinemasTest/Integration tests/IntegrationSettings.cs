using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasTest
{
    public class IntegrationSettings
    {
        public static ILunaCinemasDatabaseSettings GetCorrectSettings()
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

        public static ILunaCinemasDatabaseSettings GetIncorrectSettings()
        {
            return new LunaCinemasDatabaseSettings()
            {
                FilmsCollectionName = "film",
                ReviewsCollectionName = "review",
                CommentsCollectionName = "comment",
                ShowingsCollectionName = "showing",
                ConnectionString = "mongodb://localhost:27027",
                DatabaseName = "LunaCinemas"
            };
        }
    }
}