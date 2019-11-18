using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IUserContext
    {
        void SaveUser (User user);
        List<User> FindByEmail (string email);
        User FindById(string userId);
        void DeleteUser(string userId);

    }
    public class UserContext : IUserContext
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserContext(ILunaCinemasDatabaseSettings settings)
        {
            _userCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<User>(settings.UserCollectionName);
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
            throw new System.NotImplementedException();
        }

        public void DeleteUser(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}