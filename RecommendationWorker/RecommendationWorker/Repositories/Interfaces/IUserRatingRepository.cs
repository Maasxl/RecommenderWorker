using RecommendationWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Repositories.Interfaces
{
    public interface IUserRatingRepository
    {
        public int InsertUserRatings(List<UserRating> userRatings);
        public List<UserRating> GetUserRatingsById(string id);
        public int UpdateUserRatings(List<UserRating> userRatings);
    }
}
