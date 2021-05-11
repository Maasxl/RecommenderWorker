using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Tag
    {
        public int Start { get; set; }
        public string Event { get; set; }
        public int UniqueEventId { get; set; }
    }
}
