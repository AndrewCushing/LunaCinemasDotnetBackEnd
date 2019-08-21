using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Json;

namespace LunaCinemasBackEndInDotNet.Persistence
{

    public class FilmContext
    {
        public ActionResult<string> checkDb()
        {
            MongoClient _dbClient = new MongoClient(Properties.Resources.RealEstateConnectionString);
            IMongoDatabase database = _dbClient.GetDatabase(Properties.Resources.RealEstateDatabaseName);
            IDictionary<string, Object> stuffToReturn = new Dictionary<string, Object>();
            stuffToReturn.Add("Key1", database.ListCollections());

            return JsonParser.ToJson(stuffToReturn);
        }

    }
}