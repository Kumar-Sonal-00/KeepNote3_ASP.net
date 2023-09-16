using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Entities;
using KeepNote;
using Newtonsoft.Json;
using Xunit;

namespace Test.Controller
{
    [Collection("Db collection")]
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class CategoryControllerTest
    {
        private readonly HttpClient _client;
        public CategoryControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByUserIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            string userId = "John";
            var httpResponse = await _client.GetAsync($"/api/category/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<IEnumerable<Category>>(stringResponse);
            Assert.Contains(category, c => c.CategoryName == "Testing");
        }

        [Fact, TestPriority(2)]
        public async Task GetByIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            int categoryId = 1;
            var httpResponse = await _client.GetAsync($"/api/category/{categoryId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(stringResponse);
            Assert.Equal("Testing",category.CategoryName);
        }

        [Fact, TestPriority(2)]
        public async Task CreateCategoryShouldSuccess()
        {
            Category category = new Category { CategoryName = "API", CategoryDescription = "Microservice", CategoryCreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Category>("/api/category", category, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Category>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsAssignableFrom<Category>(response);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateCategoryShouldSuccess()
        {
            int categoryId = 1;
            Category category = new Category { CategoryId=1, CategoryName = "Testing", CategoryDescription = "Integration Testing", CategoryCreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Category>($"/api/category/{categoryId}", category, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(5)]
        public async Task DeleteCategoryShouldSuccess()
        {
            int categoryId = 2;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/category/{categoryId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }
        #endregion positivetests

        [Fact, TestPriority(6)]
        public async Task GetByIdShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            int categoryId = 3;
            var httpResponse = await _client.GetAsync($"/api/category/{categoryId}");

            // Assert that the response status code is NotFound.
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

            // If you expect a message in the response content, you can check that as well.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal($"Category with id: {categoryId} does not exist", stringResponse);
        }


        [Fact, TestPriority(7)]
        public async Task UpdateCategoryShouldFail()
        {
            int categoryId = 3;
            Category category = new Category {CategoryId=3, CategoryName = "API", CategoryDescription = "Microservice", CategoryCreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Category>($"/api/category/{categoryId}", category, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Category with id: {categoryId} does not exist", stringResponse);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteCategoryShouldFail()
        {
            int categoryId = 3;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/category/{categoryId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Category with id: {categoryId} does not exist", stringResponse);
        }
    }
}
