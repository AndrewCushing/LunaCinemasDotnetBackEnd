using System;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAccessTokenContext
    {
        bool SaveAccessToken(AccessToken token);
        AccessToken FindById(Guid accessTokenId);
        void DeleteTokenById(Guid accessTokenId);
        void DeleteByUserId(string userId);
        void DeleteAll();
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
            return _accessTokenCollection.Find(token => token.Token == accessTokenId).ToList()[0];
        }

        public void DeleteTokenById(Guid accessTokenId)
        {
            _accessTokenCollection.DeleteMany(token => token.Token == accessTokenId);
        }

        public void DeleteByUserId(string userId)
        {
            _accessTokenCollection.DeleteMany(token => token.UserId == userId);
        }

        public void DeleteAll()
        {
            _accessTokenCollection.DeleteMany(token => true);
        }
    }
}