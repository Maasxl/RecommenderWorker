using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Search
    {
        public List<Accomodation> Accomodations { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public int FlexibleDays { get; set; }
        public string FreeSearchTerm { get; set; }
        public int Timestamp { get; set; }
        public TravellingParty TravellingParty { get; set; }
    }

    public class Accomodation
    {
        public string Type { get; set; }
        public bool IsChecked { get; set; }
    }

    public class TravellingParty
    {
        public List<string> Adults { get; set; }
        public List<string> Babies { get; set; }
        public List<string> Children { get; set; }
    }
}
