using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Models.MLModels;
using RecommendationWorker.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Repositories
{
    public class CampsiteRatingRepository : ICampsiteRatingRepository
    {
        private readonly IMongoCollection<CampsiteRatingData> _campsiteRatingData;

        public CampsiteRatingRepository(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _campsiteRatingData = database.GetCollection<CampsiteRatingData>(settings.UserRatingsCollection);
        }

        public IEnumerable<CampsiteRatingData> GetAllCampsiteRatingData()
        {
            IEnumerable<CampsiteRatingData> campsiteRatingDatas = _campsiteRatingData.Find(data => true).ToList();
            if (campsiteRatingDatas.Any())
            {
                return campsiteRatingDatas;
            }
            throw new Exception("No Rating data found!");
        }
    }
}
