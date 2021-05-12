using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Campsite
    {
        [BsonElement("campsiteID")]
        public int CampsiteID { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
        [BsonElement("cpc")]
        public CPC CPC { get; set; }
        [BsonElement("indicativePrice")]
        public float IndicativePrice { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("place")]
        public string Place { get; set; }
        [BsonElement("region")]
        public string Region { get; set; }
        [BsonElement("region2")]
        public string Region2 { get; set; }
        [BsonElement("reviewScore")]
        public double ReviewScore { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
    }

    public class CPC
    {
        [BsonElement("cost")]
        public decimal Cost { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }

    }
}
