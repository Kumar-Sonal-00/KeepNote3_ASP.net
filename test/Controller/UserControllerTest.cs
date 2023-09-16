using System;
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
    public class UserControllerTest
    {
        private readonly HttpClient _client;
        public UserControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByIdShouldReturnUser()
        {
            // The endpoint or route of the controller action.
            string userId = "John";
            var httpResponse = await _client.GetAsync($"/api/user/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            Assert.Equal("John Simon", user.UserName);
        }

        [Fact, TestPriority(2)]
        public async Task RegisterUserShouldSuccess()
        {
            User user = new User { UserId = "Sam", UserName = "Sam Gomes", Password = "test123", Contact = "9876543210" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<User>("/api/user/register", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        

        [Fact, TestPriority(3)]
        public async Task LoginShouldSuccess()
        {
            User user = new User { UserId = "John", Password = "test123"};
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<User>("/api/user/login", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(4)]
        public async Task UpdateUserShouldSuccess()
        {
            string userId = "John";
            User user = new User { UserId = "John", Password = "admin123", Contact="1234567890", UserName="John Simon" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<User>($"/api/user/{userId}", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(5)]
        public async Task DeleteUserShouldSuccess()
        {
            string userId = "Sam";
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/user/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        #endregion positivetests

        #region negativetests
        [Fact, TestPriority(6)]
        public async Task GetByIdShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            string userId = "ABC";
            var httpResponse = await _client.GetAsync($"/api/user/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"User with id: {userId} does not exist", stringResponse);
        }

        [Fact, TestPriority(7)]
        public async Task RegisterUserShouldFail()
        {
            User user = new User { UserId = "John", UserName = "John Simon", Password = "test123", Contact = "9876543210" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<User>("/api/user/register", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Conflict, httpResponse.StatusCode);
            Assert.Equal($"This userid: {user.UserId} already exists", stringResponse);
        }

        [Fact, TestPriority(8)]
        public async Task LoginUserShouldFail()
        {
            User user = new User { UserId = "Sam", UserName = "Sam", Password = "test123", Contact = "9876543210" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<User>("/api/user/login", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"User with id: {user.UserId} does not exist", stringResponse);
        }

        [Fact, TestPriority(9)]
        public async Task UpdateUserShouldFail()
        {
            string userId = "Sam";
            User user = new User { UserId = "Sam", Password = "admin123", Contact = "1234567890", UserName = "Sam Kaul" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<User>($"/api/user/{userId}", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"User with id: {userId} does not exist", stringResponse);
        }

        [Fact, TestPriority(10)]
        public async Task DeleteUserShouldFail()
        {
            string userId = "Sam";
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/user/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"User with id: {userId} does not exist", stringResponse);
        }

        #endregion negativetests
    }
}
