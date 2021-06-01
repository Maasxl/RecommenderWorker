using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Models
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string UserClicksCollection { get; set; }
        public string UserRatingsCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string UserClicksCollection { get; set; }
        string UserRatingsCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
