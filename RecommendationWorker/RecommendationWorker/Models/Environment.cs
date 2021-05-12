using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Environment
    {
        [BsonElement("language")]
        public string Language { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
        [BsonElement("environment")]
        public string Stage { get; set; }
        [BsonElement("website")]
        public string Website { get; set; }
        [BsonElement("viewport")]
        public Viewport Viewport { get; set; }
        [BsonElement("deviceType")]
        public string DeviceType { get; set; }
    }

    public class Viewport
    {
        [BsonElement("width")]
        public int Width { get; set; }
        [BsonElement("height")]
        public int Height { get; set; }
    }
}
