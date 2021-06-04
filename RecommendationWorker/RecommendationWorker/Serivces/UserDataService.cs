using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Repositories.Interfaces;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IUserRatingRepository _userRatingRepository;
        private readonly IRecommendationModelSerivce _recommendationModelSerivce;

        public UserDataService(IUserDataRepository userDataRepository, IUserRatingRepository userRatingRepository, IRecommendationModelSerivce recommendationModelService)
        {
            _userDataRepository = userDataRepository;
            _userRatingRepository = userRatingRepository;
            _recommendationModelSerivce = recommendationModelService;
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
            if (data.Cookies.GA != "" || data.EntityKind != "")
            {
                // Inserts the dataLayer into MongoDB
                DataLayer returnData = _userDataRepository.InsertData(data);

                // Filters the dataLayer to get campsiteIds and adds ratings based on entityKind
                var ratingCount = FilterDataLayer(data);

                if (ratingCount > 0)
                {
                    // Train model with new data
                    _recommendationModelSerivce.TrainModel();
                }
                return returnData;
            }

            throw new Exception("No data to be saved");
        }

        public int FilterDataLayer(DataLayer data)
        {
            List<UserRating> userRatings = new List<UserRating>();
            List<UserRating> existingRatings = _userRatingRepository.GetUserRatingsById(data.Cookies.GA);
            List<UserRating> updateRatings = new List<UserRating>();
            switch (data.EntityKind)
            {
                // Campsite detail page
                case "eurocampings_campsite":
                    updateRatings = existingRatings.Where(rating => rating.CampsiteId.Equals(data.ApplicationData.BigDataObject.RequestDetail.Campsite.CampsiteID)).ToList();
                    foreach(UserRating rating in updateRatings)
                    {
                        rating.Rating = (rating.Rating + 5.0) / 2.0;
                    }

                    if (updateRatings.Count < 1)
                    {
                        userRatings.Add(new UserRating { CampsiteId = data.ApplicationData.BigDataObject.RequestDetail.Campsite.CampsiteID, UserId = data.Cookies.GA, Rating = 5.0 });
                    }
                    break;

                // Search result page
                case "eurocampings_result":
                    int id = 0;
                    foreach (string campsite in data.ApplicationData.BigDataObject.SearchResultDetail.SearchResult.Campsites)
                    {
                        if (int.TryParse(campsite, out id))
                        {
                            updateRatings = existingRatings.Where(rating => rating.CampsiteId.Equals(id)).ToList();
                            foreach (UserRating rating in updateRatings)
                            {
                                rating.Rating = (rating.Rating + 3.0) / 2.0;
                            }

                            if (updateRatings.Any(rating => rating.CampsiteId.Equals(id)) == false)
                            {
                                userRatings.Add(new UserRating { CampsiteId = id, UserId = data.Cookies.GA, Rating = 3.0 });
                            }
                        }
                    }
                    break;

                // All other pages
                default:
                    break;
            }

            int ratingCount = 0;

            if (updateRatings.Count > 0)
            {
                ratingCount += _userRatingRepository.UpdateUserRatings(updateRatings);
            }
            ratingCount += _userRatingRepository.InsertUserRatings(userRatings);
            return ratingCount;
        }
    }
}
