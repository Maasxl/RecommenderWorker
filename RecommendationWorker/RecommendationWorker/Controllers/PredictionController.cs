using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using RecommendationWorker.Models;
using RecommendationWorker.Models.MLModels;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly IRecommendationModelSerivce _recommendationModelSerivce;

        public PredictionController(IRecommendationModelSerivce recommendationModelService)
        {
            _recommendationModelSerivce = recommendationModelService;
        }

        [HttpPost]
        public ActionResult<List<CampsiteRatingPrediction>> GetPredictionforCampsites([FromBody]PredictionRequest predictionRequest)
        {
            return _recommendationModelSerivce.GetPrediciton(predictionRequest.userId, predictionRequest.campsites);
        }
    }
}
