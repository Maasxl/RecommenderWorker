using Microsoft.ML;
using Microsoft.ML.Trainers;
using RecommendationWorker.Models.MLModels;
using RecommendationWorker.Repositories.Interfaces;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Serivces
{
    public class RecommendationModelService : IRecommendationModelSerivce
    {
        private readonly ICampsiteRatingRepository _campsiteRatingRepository;

        public RecommendationModelService(ICampsiteRatingRepository campsiteRatingRepository)
        {
            _campsiteRatingRepository = campsiteRatingRepository;
        }

        public List<CampsiteRatingPrediction> GetPrediciton(string userId, int[] campsites)
        {
            MLContext mlContext = new MLContext();

            ITransformer trainedModel = mlContext.Model.Load("Data/CampsiteRecommenderModel.zip", out DataViewSchema modelSchema);

            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<CampsiteRatingData, CampsiteRatingPrediction>(trainedModel);

            List<CampsiteRatingData> campsiteData = new List<CampsiteRatingData>();
            List<CampsiteRatingPrediction> predictions = new List<CampsiteRatingPrediction>();

            foreach(int campsiteId in campsites)
            {
                campsiteData.Add(new CampsiteRatingData { UserId = userId, CampsiteId = campsiteId });
            }

            foreach (CampsiteRatingData inputData in campsiteData) 
            {
                var prediction = predictionEngine.Predict(inputData);
                if (double.IsNaN(prediction.Score))
                {
                    prediction.Score = 1;
                }
                predictions.Add(prediction);
            }

            return predictions.OrderByDescending(p => p.Score).ToList();
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
