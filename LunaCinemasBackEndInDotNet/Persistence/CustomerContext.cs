using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface ICustomerContext
    {
        void SaveUser (User user);
        List<User> FindByEmail (string email);
        User FindById(string userId);
        bool DeleteUser(string userId);
        void DeleteAll();
    }
    public class CustomerContext : ICustomerContext
    {
        private readonly IMongoCollection<User> _customerCollection;
        public CustomerContext(ILunaCinemasDatabaseSettings settings)
        {
            _customerCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<User>(settings.CustomerCollectionName);
        }
        public void SaveUser(User user)
        {
            _customerCollection.InsertOne(user);
        }

        public List<User> FindByEmail(string email)
        {
            return _customerCollection.Find(user => user.Email == email).ToList();
        }

        public User FindById(string userId)
        {
            return _customerCollection.Find(user => user.Id == userId).ToList()[0];
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                _customerCollection.DeleteOne(user => user.Id == userId);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public void DeleteAll()
        {
            _customerCollection.DeleteMany(customer => true);
        }
    }
}