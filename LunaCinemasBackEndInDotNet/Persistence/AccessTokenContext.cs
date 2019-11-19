using System;
using LunaCinemasBackEndInDotNet.Models;
using MongoDB.Driver;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public interface IAccessTokenContext
    {
        bool SaveAccessToken(AccessToken token);
        AccessToken FindById(string accessTokenId);
        void DeleteTokenById(string accessTokenId);
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

        public AccessToken FindById(string accessTokenId)
        {
            Guid tokenForComparison = Guid.Parse(accessTokenId);
            return _accessTokenCollection.Find(accessToken => accessToken.Token == tokenForComparison).ToList()[0];
        }

        public void DeleteTokenById(string accessTokenId)
        {
            Guid tokenForComparison = Guid.Parse(accessTokenId);
            _accessTokenCollection.DeleteMany(token => token.Token == tokenForComparison);
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