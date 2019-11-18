using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class SecurityService
    {
        private readonly IMongoCollection<AccessToken> _accessTokenCollection;
        public SecurityService(ILunaCinemasDatabaseSettings settings)
        {
            _accessTokenCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<AccessToken>(settings.AccessTokenCollectionName);
        }

        public string GetNewToken(string userId)
        {
            AccessToken token = new AccessToken(userId);
            _accessTokenCollection.InsertOne(token);
            return token.Token.ToString();
        }
    }
}