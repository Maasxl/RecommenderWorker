using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Campsite
    {
        public int CampsiteID { get; set; }
        public string Country { get; set; }
        public CPC CPC { get; set; }
        public float IndicativePrice { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Region { get; set; }
        public string Region2 { get; set; }
        public float ReviewScore { get; set; }
        public string Url { get; set; }
    }

    public class CPC
    {
        public decimal Cost { get; set; }
        public string Url { get; set; }

    }
}
