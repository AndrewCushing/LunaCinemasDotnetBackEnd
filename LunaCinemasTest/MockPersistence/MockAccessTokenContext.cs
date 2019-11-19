using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockAccessTokenContext : IAccessTokenContext
    {
        public bool SaveAccessToken(AccessToken token)
        {
            throw new System.NotImplementedException();
        }

        public AccessToken FindById(string accessTokenId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTokenById(string accessTokenId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }
    }
}