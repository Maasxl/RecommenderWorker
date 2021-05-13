using RecommendationWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces.Interfaces
{
    public interface IUserRatingService
    {
        public List<UserRating> GetUserRatingById(string id);
    }
}
