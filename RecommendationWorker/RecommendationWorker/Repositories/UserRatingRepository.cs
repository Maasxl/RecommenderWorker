using MongoDB.Bson;
using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Models.MLModels;
using RecommendationWorker.MongoDB;
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

        public UserRatingRepository(IMongoDBContext context)
        {
            _userRating = context.GetUserRatingCollection();
        }

        public List<UserRating> GetUserRatingsById(string id)
        {
            List<UserRating> userRatings = _userRating.Find(data => data.UserId.Equals(id)).ToList();
            return userRatings;
        }

        public int InsertUserRatings(List<UserRating> userRatings)
        {
            if (userRatings.Count > 0)
            {
                _userRating.InsertMany(userRatings);
            }
            return userRatings.Count;
        }

        public int UpdateUserRatings(List<UserRating> userRatings)
        {
            foreach(UserRating rating in userRatings)
            {
                var filter = Builders<UserRating>.Filter.Eq("UserId", rating.UserId) & Builders<UserRating>.Filter.Eq("CampsiteId", rating.CampsiteId);
                var update = Builders<UserRating>.Update.Set("Rating", rating.Rating);
                _userRating.UpdateOne(filter, update);
            }
            return userRatings.Count;
        }
    }
}
