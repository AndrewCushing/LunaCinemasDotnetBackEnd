using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest
{
    public class MockUserContext : IUserContext
    {
        private readonly List<User> users = new List<User>();
        public void SaveUser(User user)
        {
            users.Add(user);
        }

        public List<User> FindByEmail(string email)
        {
            return users.FindAll(user => user.Email.Equals(email));
        }

        public User FindById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}