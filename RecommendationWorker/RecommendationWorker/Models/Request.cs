using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Request
    {
        public string UrlHostname { get; set; }
        public string UrlPath { get; set; }
        public string PageTitle { get; set; }
        public string PageType { get; set; }
        public int Timestamp { get; set; }
    }
}
