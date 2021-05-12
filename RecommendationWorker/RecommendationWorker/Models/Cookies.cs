using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Cookies
    {
        [BsonElement("ga")]
        public string GA { get; set; }
        [BsonElement("gid")]
        public string GID { get; set; }
        [BsonElement("phpsession")]
        public string PhpSession { get; set; }
    }
}
