using RecommendationWorker.Models;
using RecommendationWorker.Repositories.Interfaces;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces
{
    public class UserRatingService : IUserRatingService
    {
        private readonly IUserRatingRepository _userRatingRepository;

        public UserRatingService(IUserRatingRepository userRatingRepository)
        {
            _userRatingRepository = userRatingRepository;
        }

        public List<UserRating> GetUserRatingById(string id)
        {
            return _userRatingRepository.GetUserRatingsById(id);
        }
    }
}
