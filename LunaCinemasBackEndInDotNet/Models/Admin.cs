namespace LunaCinemasBackEndInDotNet.Models
{
    public class Admin : User
    {
        public Admin(
            string firstName,
            string lastName,
            string email,
            string password) : base(firstName, lastName, email, password)
        {}
    }
}