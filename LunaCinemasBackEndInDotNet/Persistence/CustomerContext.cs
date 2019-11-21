using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface ICustomerContext
    {
        void Save (Customer customer);
        List<Customer> FindByEmail (string email);
        Customer FindById(string userId);
        bool DeleteCustomer(string userId);
        void DeleteAll();
        string FindByEmailAndPassword(string email, string password);
        void ChangePassword(string userId, string newPassword);
    }
    public class CustomerContext : ICustomerContext
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerContext(ILunaCinemasDatabaseSettings settings)
        {
            _customerCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<Customer>(settings.CustomerCollectionName);
        }
        
        public void Save(Customer customer)
        {
            _customerCollection.InsertOne(customer);
        }

        public List<Customer> FindByEmail(string email)
        {
            return _customerCollection.Find(user => user.Email == email).ToList();
        }

        public Customer FindById(string userId)
        {
            return _customerCollection.Find(user => user.Id == userId).ToList()[0];
        }

        public bool DeleteCustomer(string userId)
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

        public string FindByEmailAndPassword(string email, string password)
        {
            List<Customer> result = _customerCollection
                .Find(customer => customer.Email == email && customer.Password == password).ToList();
            if (result.Count > 0)
            {
                return result[0].Id;
            }
            return null;
        }

        public void ChangePassword(string userId, string newPassword)
        {
            {
                Customer replacement = _customerCollection.Find(customer => customer.Id == userId).ToList()?[0];
                replacement.Password = newPassword;
                _customerCollection.ReplaceOne(customer => customer.Id == userId, replacement);
            }
        }
    }
}