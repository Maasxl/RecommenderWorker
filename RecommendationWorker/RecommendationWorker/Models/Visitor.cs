using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Visitor
    {
        [BsonElement("cookiesAccepted")]
        public bool CookiesAccepted { get; set; }
    }
}
