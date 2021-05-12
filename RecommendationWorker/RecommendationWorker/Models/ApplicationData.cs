using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class ApplicationData
    {
        [BsonElement("bigDataObject")]
        public BigDataObject BigDataObject { get; set; }
        [BsonElement("data")]
        public List<object> Data { get; set; }
        [BsonElement("bookingData")]
        public object BookingData { get; set; }
    }
}
