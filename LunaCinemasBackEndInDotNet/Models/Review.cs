namespace LunaCinemasBackEndInDotNet.Models
{
    public class Review
    {
        public string Id { get; set; }
        public string FilmId { get; set; }
        public string UserName { get; set; }
        public string Rating { get; set; }
        public string ReviewBody { get; set; }
    }
}