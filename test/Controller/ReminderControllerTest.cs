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
    public class ReminderControllerTest
    {
        private readonly HttpClient _client;
        public ReminderControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByUserIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            string userId = "John";
            var httpResponse = await _client.GetAsync($"/api/reminder/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var reminders = JsonConvert.DeserializeObject<IEnumerable<Reminder>>(stringResponse);
            Assert.Contains(reminders, r=>r.ReminderName == "Email");
        }

        [Fact, TestPriority(2)]
        public async Task GetByIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            int reminderId = 1;
            var httpResponse = await _client.GetAsync($"/api/reminder/{reminderId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var reminder = JsonConvert.DeserializeObject<Reminder>(stringResponse);
            Assert.Equal("Email", reminder.ReminderName);
        }

        [Fact, TestPriority(2)]
        public async Task CreateReminderShouldSuccess()
        {
            Reminder reminder = new Reminder { ReminderName = "SMS", ReminderDescription = "SMS reminder", ReminderType = "notification", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Reminder>("/api/reminder", reminder, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Reminder>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsAssignableFrom<Reminder>(response);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateReminderShouldSuccess()
        {
            int reminderId = 1;
            Reminder reminder= new Reminder {ReminderId=1, ReminderName = "Email", ReminderDescription = "Mail sender", ReminderType = "notification", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Reminder>($"/api/reminder/{reminderId}", reminder, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(5)]
        public async Task DeleteReminderShouldSuccess()
        {
            int reminderId = 2;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/reminder/{reminderId}");

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
            int reminderId = 3;
            var httpResponse = await _client.GetAsync($"/api/reminder/{reminderId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Reminder with id: {reminderId} does not exist", stringResponse);
        }

        [Fact, TestPriority(7)]
        public async Task UpdateReminderShouldFail()
        {
            int reminderId = 3;
            Reminder reminder = new Reminder { ReminderId = 3, ReminderName = "SMS", ReminderDescription = "SMS sender", ReminderType = "notification", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Reminder>($"/api/reminder/{reminderId}", reminder, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Reminder with id: {reminderId} does not exist", stringResponse);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteCategoryShouldFail()
        {
            int reminderId = 3;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/reminder/{reminderId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Reminder with id: {reminderId} does not exist", stringResponse);
        }

        #endregion negativetests
    }
}
