using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Repositories;
using RecommendationWorker.Repositories.Interfaces;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserDataRepository _userDataRepository;

        public UserDataService(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public List<DataLayer> GetDataLayer()
        {
            return _userDataRepository.Get();
        }

        public DataLayer GetDataLayerById(string id)
        {
            return _userDataRepository.GetById(id);
        }

        public DataLayer InsertDataLayer(DataLayer data)
        {
            DataLayer returnData = new DataLayer();
            if (data != null)
            {
                returnData = _userDataRepository.InsertData(data);
            }

            return returnData;
        }
           
    }
}
