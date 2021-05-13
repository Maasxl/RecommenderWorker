using MongoDB.Driver;
using RecommendationWorker.Models;
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

        public int InsertUserRatings(List<UserRating> userRatings)
        {
            if (userRatings.Count > 0)
            {
                _userRating.InsertMany(userRatings);
            }
            return userRatings.Count;
        }
    }
}
