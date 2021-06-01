using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.MongoDB
{
    public interface IMongoDBContext
    {
        IMongoCollection<DataLayer> GetDatalayerCollection();
        IMongoCollection<UserRating> GetUserRatingCollection();
        IMongoCollection<CampsiteRatingData> GetCampsiteRatingDataCollection();
    }
}
