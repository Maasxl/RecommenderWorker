using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces
{
    public class UserDataService : IUserDataService
    {
        private readonly IMongoCollection<DataLayer> _dataLayer;

        public UserDataService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _dataLayer = database.GetCollection<DataLayer>(settings.UserClicksCollection);
        }

        public List<DataLayer> Get() =>
            _dataLayer.Find(data => true).ToList();

        public DataLayer InsertData(DataLayer data)
        {
            _dataLayer.InsertOne(data);
            return data;
        }
    }
}
