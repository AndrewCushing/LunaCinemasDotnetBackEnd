using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest
{
    public class MockCustomerContext : ICustomerContext
    {
        private readonly List<User> customers = new List<User>();

        public void Save(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public List<Customer> FindByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Customer FindById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteCustomer(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public string FindByEmailAndPassword(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}