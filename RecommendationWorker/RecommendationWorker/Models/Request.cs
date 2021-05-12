using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Request
    {
        [BsonElement("urlHostname")]
        public string UrlHostname { get; set; }
        [BsonElement("urlPath")]
        public string UrlPath { get; set; }
        [BsonElement("pageTitle")]
        public string PageTitle { get; set; }
        [BsonElement("pageType")]
        public string PageType { get; set; }
        [BsonElement("timestamp")]
        public long Timestamp { get; set; }
    }
}
