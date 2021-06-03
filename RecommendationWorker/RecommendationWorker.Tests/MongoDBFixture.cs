using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RecommendationWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecommendationWorker.MongoDB;
using RecommendationWorker.Serivces.Interfaces;
using RecommendationWorker.Repositories.Interfaces;
using RecommendationWorker.Repositories;
using RecommendationWorker.Serivces;
using Microsoft.Extensions.Options;

namespace RecommendationWorker.Tests
{
    public class MongoDBFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected IConfiguration Configuration { get; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

                services.Configure<MongoDatabaseSettings>(config.GetSection("Testing_MongoDatabaseSettings"));
                
                services.AddSingleton<IMongoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

                services.AddScoped<IUserDataService, UserDataService>();
                services.AddScoped<IUserDataRepository, UserDataRepository>();
                services.AddScoped<IUserRatingRepository, UserRatingRepository>();
                services.AddScoped<IUserRatingService, UserRatingService>();
                services.AddScoped<ICampsiteRatingRepository, CampsiteRatingRepository>();
                services.AddScoped<IRecommendationModelSerivce, RecommendationModelService>();
                services.AddScoped<IMongoDBContext, MongoDBContext>();

                services.AddControllers();
            });
        }
    }
}
