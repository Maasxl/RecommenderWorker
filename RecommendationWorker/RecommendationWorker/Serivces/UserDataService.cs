using Microsoft.ML;
using Microsoft.ML.Trainers;
using MongoDB.Driver;
using RecommendationWorker.Models;
using RecommendationWorker.Models.MLModels;
using RecommendationWorker.Repositories;
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
        private readonly ICampsiteRatingRepository _campsiteRatingRepository;

        public UserDataService(IUserDataRepository userDataRepository, IUserRatingRepository userRatingRepository, ICampsiteRatingRepository campsiteRatingRepository)
        {
            _userDataRepository = userDataRepository;
            _userRatingRepository = userRatingRepository;
            _campsiteRatingRepository = campsiteRatingRepository;
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
                FilterDataLayer(data);

                // TODO: Train model with new data
                TrainModel();
            }

            return returnData;
        }

        public void FilterDataLayer(DataLayer data)
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

                            if (updateRatings.Any(rating => rating.CampsiteId.Equals(campsite)))
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

            if (updateRatings.Count > 0)
            {
                _userRatingRepository.UpdateUserRatings(updateRatings);
            }
            _userRatingRepository.InsertUserRatings(userRatings);
        }

        public void TrainModel()
        {
            MLContext mlContext = new MLContext();

            // Get training data
            IEnumerable<CampsiteRatingData> ratings = _campsiteRatingRepository.GetAllCampsiteRatingData();
            IDataView campsiteRatingData = mlContext.Data.LoadFromEnumerable(ratings);

            // Split data in train en test sets
            DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(campsiteRatingData, testFraction: 0.2);
            IDataView trainData = dataSplit.TrainSet;
            IDataView testData = dataSplit.TestSet;

            // Define the model
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "UserId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "campsiteIdEncoded", inputColumnName: "CampsiteId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "campsiteIdEncoded",
                LabelColumnName = "Rating",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            // Train the model
            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));
            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainData);

            // Evaluate model
            Console.WriteLine("=============== Evaluating the model ===============");

            var prediction = model.Transform(testData);
            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "CampsiteId", scoreColumnName: "Rating");

            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());

            // Save the model
            var modelPath = Path.Combine(System.Environment.CurrentDirectory, "Data", "CampsiteRecommenderModel.zip");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(model, trainData.Schema, modelPath);
            Console.WriteLine("Model is saved in " + modelPath);
        }
    }
}
