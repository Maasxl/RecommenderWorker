using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RecommendationWorker.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecommendationWorker.Tests
{
    public class UserDataControllerTest : IClassFixture<MongoDBFixture<Startup>>
    {
        private readonly MongoDBFixture<Startup> _factory;
        private readonly HttpClient _client;

        public UserDataControllerTest(MongoDBFixture<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/userdata")]
        public async Task PostUserDataSuccess(string url)
        {
            var obj = new DataLayer { Cookies = new Cookies { GA = "GA1_test" }, EntityKind = "eurocampings_campsite", ApplicationData = new ApplicationData { BigDataObject = new BigDataObject { RequestDetail = new RequestDetail { Campsite = new Campsite { CampsiteID = 1 } } } } };
            var mockPayload = JsonConvert.SerializeObject(obj);

            HttpContent request = new StringContent(mockPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, request);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/plain; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
            Assert.True(response.Content.ReadAsStringAsync().Result != "" && response.Content.ReadAsStringAsync().Result != null);
        }

        [Theory]
        [InlineData("/api/userdata")]
        [InlineData("/api/userdata/GA1_test")]
        [InlineData("/api/userrating/GA1_test")]
        public async Task GetDataLayersSuccess(string url)
        {            
            var response = await _client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/userdata/1")]
        [InlineData("/api/userrating/1")]
        public async Task GetDataLayersWithIdNotFound(string url)
        {
            var response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/userdata")]
        public async Task PostUserDataFailed(string url)
        {
            var obj = new DataLayer { EntityKind = "eurocampings_campsite", ApplicationData = new ApplicationData { BigDataObject = new BigDataObject { RequestDetail = new RequestDetail { Campsite = new Campsite { CampsiteID = 1 } } } } };
            var mockPayload = JsonConvert.SerializeObject(obj);

            HttpContent request = new StringContent(mockPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, request);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
