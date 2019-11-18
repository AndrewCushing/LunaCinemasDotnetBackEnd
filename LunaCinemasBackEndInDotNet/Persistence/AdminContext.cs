using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAdminContext
    {
        void Save(User user);
        List<User> FindByEmail(string email);
        User FindById(string userId);
        bool DeleteUser(string userId);
        void DeleteAll();
    }
    public class AdminContext : IAdminContext
    {
        private readonly IMongoCollection<User> _adminCollection;
        public AdminContext(ILunaCinemasDatabaseSettings settings)
        {
            _adminCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<User>(settings.AdminCollectionName);
        }
        public void Save(User user)
        {
            _adminCollection.InsertOne(user);
        }

        public List<User> FindByEmail(string email)
        {
            return _adminCollection.Find(user => user.Email == email).ToList();
        }

        public User FindById(string userId)
        {
            return _adminCollection.Find(user => user.Id == userId).ToList()[0];
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                _adminCollection.DeleteOne(user => user.Id == userId);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public void DeleteAll()
        {
            _adminCollection.DeleteMany(admin => true);
        }
    }
}