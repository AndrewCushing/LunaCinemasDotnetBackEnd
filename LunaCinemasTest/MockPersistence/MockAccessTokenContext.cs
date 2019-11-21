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

        public AccessToken FindByGuid(string accessTokenId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTokenByGuid(string accessTokenId)
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