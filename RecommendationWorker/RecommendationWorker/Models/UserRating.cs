using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class UserRating
    {
        public string UserId { get; set; }
        public int CampsiteId { get; set; }
        public double Rating { get; set; }
    }
}
