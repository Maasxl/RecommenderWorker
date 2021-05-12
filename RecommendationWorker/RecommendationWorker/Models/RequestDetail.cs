using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class RequestDetail
    {
        [BsonElement("campsite")]
        public Campsite Campsite { get; set; }
        [BsonElement("environment")]
        public Environment Environment { get; set; }
        [BsonElement("request")]
        public Request Request { get; set; }
        [BsonElement("visitor")]
        public Visitor Visitor { get; set; }
    }
}
