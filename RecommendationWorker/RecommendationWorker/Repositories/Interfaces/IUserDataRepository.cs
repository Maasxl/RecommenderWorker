using RecommendationWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Repositories.Interfaces
{
    public interface IUserDataRepository
    {
        List<DataLayer> Get();
        DataLayer GetById(string id);
        DataLayer InsertData(DataLayer data);
    }
}
