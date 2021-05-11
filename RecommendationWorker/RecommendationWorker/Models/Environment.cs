using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Environment
    {
        public string Language { get; set; }
        public string Country { get; set; }
        public string Stage { get; set; }
        public string Website { get; set; }
        public Viewport Viewport { get; set; }
        public string DeviceType { get; set; }
    }

    public class Viewport
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
