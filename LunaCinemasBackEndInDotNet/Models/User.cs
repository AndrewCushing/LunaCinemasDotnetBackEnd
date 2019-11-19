using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunaCinemasBackEndInDotNet.Models
{
    public abstract class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        protected User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            if (!SetPassword(password))
            {
                throw new InvalidDataException("Password not hashed correctly");
            }
        }

        protected bool ChangePassword(string newPassword)
        {
            if (Password != newPassword)
            {
                return SetPassword(newPassword);
            }
            return false;
        }

        private bool SetPassword(string password)
        {
            if (password.Length == 64)
            {
                Password = password;
                return true;
            }
            return false;
        }
    }
}