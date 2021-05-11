using RecommendationWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces.Interfaces
{
    interface IUserDataService
    {
        List<DataLayer> Get();
        DataLayer InsertData(DataLayer data);
    }
}
