using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSC.FileStorageData
{
    public class BaseRepo
    {
        protected static IMongoClient _mongoClient;
        protected static string _databaseName;
        private readonly IConfiguration _config;

        public BaseRepo(IConfiguration config)
        {
            _config = config;
            if (_mongoClient == null)
            {
                var connectionString = config.GetConnectionString("DefaultMongoConnection");
                var url = MongoUrl.Create(connectionString);
                _databaseName = url.DatabaseName;
                if (_mongoClient == null)
                    _mongoClient = new MongoClient(url);
            }
        }

        protected IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(_databaseName);
        }

    }
}
