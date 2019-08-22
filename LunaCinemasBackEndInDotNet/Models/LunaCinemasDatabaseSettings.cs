namespace LunaCinemasBackEndInDotNet.Models
{
    public class LunaCinemasDatabaseSettings : ILunaCinemasDatabaseSettings
    {
        public string FilmsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILunaCinemasDatabaseSettings
    {
        string FilmsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}