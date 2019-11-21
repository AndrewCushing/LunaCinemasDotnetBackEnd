using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockAdminContext : IAdminContext
    {
        private readonly List<User> admins = new List<User>();

        public void Create(Admin user)
        {
            throw new System.NotImplementedException();
        }

        public List<Admin> FindByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Admin FindById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAdmin(string userId)
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