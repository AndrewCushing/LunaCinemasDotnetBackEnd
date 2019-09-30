using System;
using Microsoft.AspNetCore.Authentication.Twitter;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAccessTokenContext
    {
        void SaveAccessToken();
        AccessToken FindById(Guid accessTokenId);
        void DeleteTokenById(Guid accessTokenId);
    }

    public class AccessTokenContext : IAccessTokenContext
    {
        public void SaveAccessToken()
        {
            throw new NotImplementedException();
        }

        public AccessToken FindById(Guid accessTokenId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTokenById(Guid accessTokenId)
        {
            throw new NotImplementedException();
        }
    }
}