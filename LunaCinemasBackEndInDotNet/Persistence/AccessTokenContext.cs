using System;
using Microsoft.AspNetCore.Authentication.Twitter;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAccessTokenContext
    {
        bool SaveAccessToken();
        AccessToken FindById(Guid accessTokenId);
        bool DeleteTokenById(Guid accessTokenId);
        bool DeleteByUserId(string userId);

    }

    public class AccessTokenContext : IAccessTokenContext
    {
        public bool SaveAccessToken()
        {
            throw new NotImplementedException();
        }

        public AccessToken FindById(Guid accessTokenId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTokenById(Guid accessTokenId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}