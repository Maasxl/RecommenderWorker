using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Tag
    {
        [BsonElement("gtm.start")]
        public int Start { get; set; }
        [BsonElement("event")]
        public string Event { get; set; }
        [BsonElement("gtm.uniqueEventId")]
        public int UniqueEventId { get; set; }
    }
}
