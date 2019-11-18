using System;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAccessTokenContext
    {
        bool SaveAccessToken(AccessToken token);
        AccessToken FindById(Guid accessTokenId);
        bool DeleteTokenById(Guid accessTokenId);
        bool DeleteByUserId(string userId);

    }

    public class AccessTokenContext : IAccessTokenContext
    {
        private readonly IMongoCollection<AccessToken> _accessTokenCollection;

        public AccessTokenContext(ILunaCinemasDatabaseSettings settings)
        {
            _accessTokenCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<AccessToken>(settings.AccessTokenCollectionName);
        }

        public bool SaveAccessToken(AccessToken token)
        {
            try
            {
                _accessTokenCollection.InsertOne(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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