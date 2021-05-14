using RecommendationWorker.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces.Interfaces
{
    public interface IRecommendationModelSerivce
    {
        public void TrainModel();
        public List<CampsiteRatingPrediction> GetPrediciton(string userId, int[] campsites);
    }
}
