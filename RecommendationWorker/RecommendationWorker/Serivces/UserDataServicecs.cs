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
        private readonly IUserRatingRepository _userRatingRepository;

        public UserDataService(IUserDataRepository userDataRepository, IUserRatingRepository userRatingRepository)
        {
            _userDataRepository = userDataRepository;
            _userRatingRepository = userRatingRepository;
        }

        public List<DataLayer> GetDataLayer()
        {
            return _userDataRepository.Get();
        }

        public List<DataLayer> GetDataLayerByUserId(string id)
        {
            return _userDataRepository.GetByUserId(id);
        }

        public DataLayer InsertDataLayer(DataLayer data)
        {
            DataLayer returnData = new DataLayer();
            if (data != null)
            {
                // Inserts the dataLayer into MongoDB
                returnData = _userDataRepository.InsertData(data);

                // Filters the dataLayer to get campsiteIds and adds ratings based on entityKind
                int amount = _userRatingRepository.InsertUserRatings(FilterDataLayer(data));
            }

            return returnData;
        }

        private List<UserRating> FilterDataLayer(DataLayer data)
        {
            List<UserRating> userRatings = new List<UserRating>();
            switch (data.EntityKind)
            {
                // Campsite detail page
                case "eurocampings_campsite":
                    userRatings.Add(new UserRating { CampsiteId = data.ApplicationData.BigDataObject.RequestDetail.Campsite.CampsiteID, UserId = data.Cookies.GA, Rating = 5.0 });
                    break;

                // Search result page
                case "eurocampings_result":
                    foreach (string campsite in data.ApplicationData.BigDataObject.SearchResultDetail.SearchResult.Campsites)
                    {
                        if (int.TryParse(campsite, out int id))
                        {
                            userRatings.Add(new UserRating { CampsiteId = id, UserId = data.Cookies.GA, Rating = 3.0 });
                        }
                    }
                    break;

                // All other pages
                default:
                    break;
            }
            return userRatings;
        }
           
    }
}
