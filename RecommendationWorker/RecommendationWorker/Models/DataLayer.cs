﻿using MongoDB.Bson;
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
        [BsonElement("timestamp")]
        public long Timestamp { get; set; }
        [BsonElement("applicationData")]
        public ApplicationData ApplicationData { get; set; }
        [BsonElement("entityKind")]
        public string EntityKind { get; set; }
        [BsonElement("ip")]
        public string Ip { get; set; }
        [BsonElement("cookies")]
        public Cookies Cookies { get; set; }
    }
}
