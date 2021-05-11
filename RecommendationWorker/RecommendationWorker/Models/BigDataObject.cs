using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class BigDataObject
    {
        public RequestDetail Request { get; set; }
        public string ClangHash { get; set; }
        public Search Search { get; set; }
        public List<int> Campsites { get; set; }
    }
}
