using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models.MLModels
{
    public class CampsiteRatingData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public float CampsiteId { get; set; }
        public float Rating { get; set; }
    }

    public class CampsiteRatingPrediction
    {
        public float CampsiteId { get; set; }
        public float Score { get; set; }
    }
}
