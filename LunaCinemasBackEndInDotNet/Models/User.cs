namespace LunaCinemasBackEndInDotNet.Models
{
    public class User
    {
        public string id { get; set; }
        public string Username { get; }
        public string Password { get; }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}