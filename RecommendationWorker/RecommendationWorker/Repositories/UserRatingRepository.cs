using MongoDB.Bson;
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
    public class UserRatingRepository : IUserRatingRepository
    {
        private readonly IMongoCollection<UserRating> _userRating;

        public UserRatingRepository(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userRating = database.GetCollection<UserRating>(settings.UserRatingsCollection);
        }

        public List<UserRating> GetUserRatingsById(string id)
        {
            return _userRating.Find(data => data.UserId.Equals(id)).ToList();
        }

        public int InsertUserRatings(List<UserRating> userRatings)
        {
            if (userRatings.Count > 0)
            {
                _userRating.InsertMany(userRatings);
            }
            return userRatings.Count;
        }

        public void UpdateUserRatings(List<UserRating> userRatings)
        {
            if (userRatings.Count > 0)
            {
                foreach(UserRating rating in userRatings)
                {
                    var filter = Builders<UserRating>.Filter.Eq("UserId", rating.UserId) & Builders<UserRating>.Filter.Eq("CampsiteId", rating.CampsiteId);
                    var update = Builders<UserRating>.Update.Set("Rating", rating.Rating);
                    _userRating.UpdateOne(filter, update);
                }
            }
        }
    }
}
