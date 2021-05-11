using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class DataLayer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Timestamp { get; set; }
        public ApplicationData ApplicationData { get; set; }
        public string Entity_Kind { get; set; }
        public string Ip { get; set; }
        public Cookies Cookies { get; set; }
    }
}
