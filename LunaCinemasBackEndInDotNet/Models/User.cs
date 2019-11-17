using System.IO;

namespace LunaCinemasBackEndInDotNet.Models
{
    public abstract class User
    {
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
            SetPassword(password);
        }

        protected bool ChangePassword(string newPassword)
        {
            try
            {
                SetPassword(newPassword);
                return true;
            }
            catch (InvalidDataException)
            {
                return false;
            }
        }

        private void SetPassword(string password)
        {
            if (password.Length == 64)
            {
                Password = password;
            }
            throw new InvalidDataException("Password was not hashed appropriately.");
        }
    }
}