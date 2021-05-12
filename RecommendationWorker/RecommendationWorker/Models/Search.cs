using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class Search
    {
        [BsonElement("accomodations")]
        public List<Accomodation> Accomodations { get; set; }
        [BsonElement("arrivalDate")]
        public string ArrivalDate { get; set; }
        [BsonElement("datetime")]
        public string DateTime { get; set; }
        [BsonElement("duration")]
        public int Duration { get; set; }
        [BsonElement("flexibleDays")]
        public int FlexibleDays { get; set; }
        [BsonElement("freesearchTerm")]
        public string FreeSearchTerm { get; set; }
        [BsonElement("timeStamp")]
        public long Timestamp { get; set; }
        [BsonElement("travellingParty")]
        public TravellingParty TravellingParty { get; set; }
    }

    public class Accomodation
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("isChecked")]
        public bool IsChecked { get; set; }
    }

    public class TravellingParty
    {
        [BsonElement("adults")]
        public List<string> Adults { get; set; }
        [BsonElement("babies")]
        public List<string> Babies { get; set; }
        [BsonElement("children")]
        public List<string> Children { get; set; }
    }
}
