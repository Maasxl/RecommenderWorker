using RecommendationWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces.Interfaces
{
    public interface IUserDataService
    {
        List<DataLayer> GetDataLayer();
        DataLayer GetDataLayerById(string id);
        DataLayer InsertDataLayer(DataLayer data);
    }
}
