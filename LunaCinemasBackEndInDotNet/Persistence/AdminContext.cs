using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAdminContext
    {
        void SaveUser(User user);
        List<User> FindByEmail(string email);
        User FindById(string userId);
        bool DeleteUser(string userId);

    }
    public class AdminContext : IAdminContext
    {
        private readonly IMongoCollection<User> _userCollection;
        public AdminContext(ILunaCinemasDatabaseSettings settings)
        {
            _userCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<User>(settings.AdminCollectionName);
        }
        public void SaveUser(User user)
        {
            _userCollection.InsertOne(user);
        }

        public List<User> FindByEmail(string email)
        {
            return _userCollection.Find(user => user.Email == email).ToList();
        }

        public User FindById(string userId)
        {
            return _userCollection.Find(user => user.Id == userId).ToList()[0];
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                _userCollection.DeleteOne(user => user.Id == userId);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}