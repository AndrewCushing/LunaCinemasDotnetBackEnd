using System;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class SecurityService
    {
        private readonly IAccessTokenContext _accessTokenContext;
        public SecurityService(IAccessTokenContext accessTokenContext)
        {
            _accessTokenContext = accessTokenContext;
        }

        public string GetNewToken(string userId)
        {
            _accessTokenContext.DeleteByUserId(userId);
            AccessToken token = new AccessToken(userId);
            _accessTokenContext.SaveAccessToken(token);
            return token.Token.ToString();
        }

        public bool ValidateToken(string tokenId)
        {
            AccessToken accessToken = _accessTokenContext.FindByGuid(tokenId);
            
            if (accessToken == null)
            {
                return false;
            }

            if (accessToken.ExpiryTime.CompareTo(DateTime.Now) < 0)
            {
                _accessTokenContext.DeleteTokenByGuid(tokenId);
            }
            accessToken.ExpiryTime = DateTime.Now.AddMinutes(AccessToken.MinutesBeforeTokenExpiry);
            return true;
        }

        public void DeleteToken(string token)
        {
            _accessTokenContext.DeleteTokenByGuid(token);
        }

        public string GetUserIdFromToken(string token)
        {
            return _accessTokenContext.FindByGuid(token)?.UserId;
        }
    }
}