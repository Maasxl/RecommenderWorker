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
        List<DataLayer> GetByUserId(string id);
        DataLayer InsertData(DataLayer data);
    }
}
