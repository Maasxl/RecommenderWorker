using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class RequestDetail
    {
        public Campsite Campsite { get; set; }
        public Environment Environment { get; set; }
        public Request Request { get; set; }
        public Visitor Visitor { get; set; }
    }
}
