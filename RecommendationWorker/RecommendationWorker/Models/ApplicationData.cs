using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class ApplicationData
    {
        public BigDataObject BigDataObject { get; set; }
        public object[] Data { get; set; }
        public object BookingData { get; set; }
    }
}
