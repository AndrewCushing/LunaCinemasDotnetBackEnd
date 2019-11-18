using LunaCinemasBackEndInDotNet.BusinessLogic;

namespace LunaCinemasBackEndInDotNet.Models
{
    public class LunaCinemasDatabaseSettings : ILunaCinemasDatabaseSettings
    {
        public string CommentsCollectionName { get; set; }
        public string ReviewsCollectionName { get; set; }
        public string FilmsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BookingCollectionName { get; set; }
        public string AccessTokenCollectionName { get; set; }
        public string CustomerCollectionName { get; set; }
        public string AdminCollectionName { get; set; }
        public string ShowingsCollectionName { get; set; }
    }

    public interface ILunaCinemasDatabaseSettings
    {
        string CommentsCollectionName { get; set; }
        string ReviewsCollectionName { get; set; }
        string FilmsCollectionName { get; set; }
        string ShowingsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string BookingCollectionName { get; set; }
        string AccessTokenCollectionName { get; set; }
        string CustomerCollectionName { get; set; }
        string AdminCollectionName { get; set; }
    }
}