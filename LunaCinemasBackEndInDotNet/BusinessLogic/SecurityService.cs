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
            AccessToken token = new AccessToken(userId);
            _accessTokenContext.SaveAccessToken(token);
            return token.Token.ToString();
        }
    }
}