using MongoDB.Bson;
using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly IMongoCollection<DataLayer> _dataLayer;

        public UserDataRepository(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _dataLayer = database.GetCollection<DataLayer>(settings.UserClicksCollection);
        }

        public List<DataLayer> Get()
        {
            List<DataLayer> UserData = _dataLayer.Find(_ => true).ToList();
            if (UserData.Count > 0)
            {
                return UserData;
            } 
            else
            {
                throw new Exception("Cannot get any results");
            }
        }

        public List<DataLayer> GetByUserId(string id)
        {
            List<DataLayer> UserData = _dataLayer.Find(data => data.Cookies.GA.Equals(id) || data.Cookies.GID.Equals(id)).ToList();
            if (UserData.Count > 0)
            {
                return UserData;
            } 
            else
            {
                throw new Exception("No results found");
            }
        }

        public DataLayer InsertData(DataLayer data)
        {
            _dataLayer.InsertOne(data);
            return data;
        }
    }
}
