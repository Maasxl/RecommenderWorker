using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RecommendationWorker.Tests
{
    public class UserDataControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UserDataControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/userdata")]
        [InlineData("/api/userdata/GA1.2.1398516428.1612957055")]
        [InlineData("/api/userrating/GA1.2.1398516428.1612957055")]
        public async Task GetDataLayersSuccess(string url)
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/userdata/1")]
        [InlineData("/api/userrating/1")]
        public async Task GetDataLayersWithIdNotFound(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
