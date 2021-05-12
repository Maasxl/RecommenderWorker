using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class BigDataObject
    {
        [BsonElement("requestDetail")]
        public RequestDetail RequestDetail { get; set; }
        [BsonElement("clangHash")]
        public string ClangHash { get; set; }
        [BsonElement("search")]
        public Search Search { get; set; }
        [BsonElement("searchResultDetail")]
        public SearchResultDetail SearchResultDetail { get; set; }
    }

    public class SearchResultDetail
    {
        [BsonElement("searchResult")]
        public SearchResult SearchResult { get; set; }
    }

    public class SearchResult
    {
        [BsonElement("campsites")]
        public string[] Campsites { get; set; }
    }
}
