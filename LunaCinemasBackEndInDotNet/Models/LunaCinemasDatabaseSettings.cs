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
    }

    public interface ILunaCinemasDatabaseSettings
    {
        string CommentsCollectionName { get; set; }
        string ReviewsCollectionName { get; set; }
        string FilmsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}