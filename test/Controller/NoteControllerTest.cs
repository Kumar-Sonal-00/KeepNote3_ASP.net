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
    public class NoteControllerTest
    {
        private readonly HttpClient _client;
        public NoteControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByUserIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            string userId = "John";
            var httpResponse = await _client.GetAsync($"/api/note/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            Assert.Contains(notes, n => n.NoteTitle == "Technology");
        }

        [Fact, TestPriority(2)]
        public async Task GetByIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            int noteId = 1;
            var httpResponse = await _client.GetAsync($"/api/note/{noteId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<Note>(stringResponse);
            Assert.Equal("Technology", note.NoteTitle);
        }

        [Fact, TestPriority(2)]
        public async Task CreateNoteShouldSuccess()
        {
            Note note = new Note { CategoryId = 1, ReminderId = 1, NoteTitle = "Tech-Stack", NoteContent = "DotNet", NoteStatus = "Started", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Note>("/api/note", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Note>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsAssignableFrom<Note>(response);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateNoteShouldSuccess()
        {
            int noteId = 1;
            Note note = new Note { CategoryId = 1, ReminderId = 1, NoteTitle = "Technology", NoteContent = "DotNet Core", NoteStatus = "Completed", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Note>($"/api/note/{noteId}", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(5)]
        public async Task DeleteNoteShouldSuccess()
        {
            int noteId = 2;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/note/{noteId}");

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
            int noteId = 3;
            var httpResponse = await _client.GetAsync($"/api/note/{noteId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Note with id: {noteId} does not exist", stringResponse);
        }

        [Fact, TestPriority(7)]
        public async Task CreateNoteShouldFailWithInvalidCategory()
        {
            Note note = new Note { CategoryId = 2, ReminderId = 1, NoteTitle = "Tech-Stack", NoteContent = "DotNet", NoteStatus = "Started", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Note>("/api/note", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
           // var response = JsonConvert.DeserializeObject<Note>(stringResponse);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal($"Category with id: {note.CategoryId} does not exist", stringResponse);
        }

        [Fact, TestPriority(8)]
        public async Task CreateNoteShouldFailWithInvalidReminder()
        {
            Note note = new Note { CategoryId = 1, ReminderId = 2, NoteTitle = "Tech-Stack", NoteContent = "DotNet", NoteStatus = "Started", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Note>("/api/note", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal($"Reminder with id: {note.ReminderId} does not exist", stringResponse);
        }

        [Fact, TestPriority(9)]
        public async Task UpdateNoteShouldFailInvalidNoteId()
        {
            int noteId = 3;
            Note note = new Note {NoteId=3, CategoryId = 1, ReminderId = 1, NoteTitle = "Technology", NoteContent = "DotNet Core", NoteStatus = "Completed", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Note>($"/api/note/{noteId}", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"Note with id: {noteId} does not exist", stringResponse);
        }

        [Fact, TestPriority(10)]
        public async Task UpdateNoteShouldFailInvalidCategoryId()
        {
            int noteId = 1;
            Note note = new Note { NoteId = 1, CategoryId = 2, ReminderId = 1, NoteTitle = "Technology", NoteContent = "DotNet Core", NoteStatus = "Completed", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Note>($"/api/note/{noteId}", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal($"Category with id: {note.CategoryId} does not exist", stringResponse);
        }

        [Fact, TestPriority(11)]
        public async Task UpdateNoteShouldFailInvalidReminderId()
        {
            int noteId = 1;
            Note note = new Note { NoteId = 1, CategoryId = 1, ReminderId = 2, NoteTitle = "Technology", NoteContent = "DotNet Core", NoteStatus = "Completed", CreatedBy = "John" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Note>($"/api/note/{noteId}", note, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal($"Reminder with id: {note.ReminderId} does not exist", stringResponse);
        }
        #endregion negativetests
    }
}
