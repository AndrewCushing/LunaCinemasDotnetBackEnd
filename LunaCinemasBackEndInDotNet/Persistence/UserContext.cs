using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IUserContext
    {
        void SaveUser (User user);
        List<User> FindByUsername (string username);
        User FindById(string userId);

    }
    public class UserContext : IUserContext
    {
        public void SaveUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public List<User> FindByUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public User FindById(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}