using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class PredictionRequest
    {
        [BsonElement("userId")]
        public string userId { get; set; }
        [BsonElement("campsites")]
        public int[] campsites { get; set; }
    }
}
