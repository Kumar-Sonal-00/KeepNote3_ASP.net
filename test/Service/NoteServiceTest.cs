using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;
using Moq;
using Service;
using Xunit;
using System;

namespace Test.Service
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class NoteServiceTest
    {
        Mock<INoteRepository> mockNoteRepo;
        Mock<ICategoryRepository> mockCategoryRepo;
        Mock<IReminderRepository> mockReminderRepo;

        public NoteServiceTest()
        {
            mockNoteRepo = new Mock<INoteRepository>();
            mockCategoryRepo = new Mock<ICategoryRepository>();
            mockReminderRepo = new Mock<IReminderRepository>();
        }

        #region positivetests
        [Fact]
        public void GetAllNotesByUserIdShouldReturnList()
        {
            var userId = "John";

            mockNoteRepo.Setup(repo => repo.GetAllNotesByUserId(userId)).Returns(this.GetNotes());
            var service = new NoteService(mockNoteRepo.Object,mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = service.GetAllNotesByUserId(userId);

            Assert.IsAssignableFrom<IEnumerable<Note>>(actual);
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetNoteByNoteIdShouldReturnNote()
        {
            var noteId = 1;
            var note = new Note { CategoryId = 1, ReminderId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Started", CreatedBy = "John" };
            mockNoteRepo.Setup(repo => repo.GetNoteByNoteId(noteId)).Returns(note);
            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = service.GetNoteByNoteId(noteId);
            Assert.IsAssignableFrom<Note>(actual);
            Assert.Equal("Technology", actual.NoteTitle);
        }

        [Fact]
        public void CreateNoteShouldReturnNote()
        {
            Note note = new Note {NoteId=2, CategoryId = 1, ReminderId = 1, NoteTitle = "Tech-Stack", NoteContent = "DotNet", NoteStatus = "Started", CreatedBy = "John" };
            Reminder reminder = new Reminder { ReminderId = 1, ReminderName = "Email", ReminderDescription = "Email reminder", ReminderType = "notification", CreatedBy = "John" };
            Category category = new Category { CategoryName = "API", CategoryDescription = "Microservice", CategoryCreatedBy = "John", CategoryCreationDate = new DateTime() };

            mockCategoryRepo.Setup(repo => repo.GetCategoryById(1)).Returns(category);
            mockReminderRepo.Setup(repo => repo.GetReminderById(1)).Returns(reminder);
            mockNoteRepo.Setup(repo => repo.CreateNote(note)).Returns(note);
            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = service.CreateNote(note);
            Assert.IsAssignableFrom<Note>(actual);
            Assert.Equal("Tech-Stack", actual.NoteTitle);
            Assert.Equal(2, actual.NoteId);
        }

        [Fact]
        public void DeleteNoteShouldSuccess()
        {
            int noteId = 2;
            mockNoteRepo.Setup(repo => repo.DeleteNote(noteId)).Returns(true);
            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = service.DeleteNote(noteId);
            Assert.True(actual);
        }

        [Fact]
        public void UpdateNoteShouldSuccess()
        {
            var note = new Note {NoteId=1, CategoryId = 1, ReminderId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET MVC Core", NoteStatus = "Started", CreatedBy = "John" };
            Reminder reminder = new Reminder { ReminderId = 1, ReminderName = "Email", ReminderDescription = "Email reminder", ReminderType = "notification", CreatedBy = "John" };
            Category category = new Category { CategoryName = "API", CategoryDescription = "Microservice", CategoryCreatedBy = "John", CategoryCreationDate = new DateTime() };

            mockCategoryRepo.Setup(repo => repo.GetCategoryById(1)).Returns(category);
            mockReminderRepo.Setup(repo => repo.GetReminderById(1)).Returns(reminder);
            mockNoteRepo.Setup(repo => repo.GetNoteByNoteId(1)).Returns(note);
            mockNoteRepo.Setup(repo => repo.UpdateNote(1,note)).Returns(true);

            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = service.UpdateNote(1, note);
            Assert.True(actual);
        }

        #endregion positivetests
        private List<Note> GetNotes()
        {
            return new List<Note> { new Note { CategoryId = 1, ReminderId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Started", CreatedBy = "John" } };
        }

        #region negativetests
        [Fact]
        public void GetAllNotesShouldReturnEmptyList()
        {
            string userId = "Sam";
            mockNoteRepo.Setup(repo => repo.GetAllNotesByUserId(userId)).Returns(new List<Note>());
            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = service.GetAllNotesByUserId(userId);
            Assert.IsAssignableFrom<IEnumerable<Note>>(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetNoteByIdShouldThrowException()
        {
            int noteId = 2;
            Note note = null;

            // Configure the mockNoteRepo to return null when GetNoteByNoteId is called.
            mockNoteRepo.Setup(repo => repo.GetNoteByNoteId(noteId)).Returns(note);

            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            // Use Assert.Throws<NoteNotFoundException> to check for the exception
            var actual = Assert.Throws<Exceptions.NoteNotFoundException>(() => service.GetNoteByNoteId(noteId));

            // Assert that the exception message is as expected
            Assert.Equal($"Note with id: {noteId} does not exist", actual.Message);
        }



        [Fact]
        public void CreateNoteShouldThrowException()
        {
            // Create a note with CategoryId = 2
            Note note = new Note { CategoryId = 2, ReminderId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Started", CreatedBy = "John" };

            // Create the NoteService with the mock repositories
            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            // Use Assert.Throws<CategoryNotFoundException> to check for the exception
            var actual = Assert.Throws<Exceptions.CategoryNotFoundException>(() => service.CreateNote(note));

            // Assert that the exception message is as expected
            Assert.Equal($"Category with id: {note.CategoryId} does not exist", actual.Message);
        }




        [Fact]
        public void UpdateNoteShouldThrowException()
        {
            int noteId = 1;
            Note note = new Note { CategoryId = 2, ReminderId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Started", CreatedBy = "John" };
            Category category = null;
            Reminder reminder = new Reminder { ReminderId = 1, ReminderName = "Email", ReminderDescription = "Email reminder", ReminderType = "notification", CreatedBy = "John" };

            // Configure the mockNoteRepo to return a valid note.
            mockNoteRepo.Setup(repo => repo.GetNoteByNoteId(noteId)).Returns(note);

            // Configure the mockCategoryRepo to throw a CategoryNotFoundException.
            mockCategoryRepo.Setup(repo => repo.GetCategoryById(note.CategoryId)).Throws(new CategoryNotFoundException($"Category with id: {note.CategoryId} does not exist"));

            // Configure the mockReminderRepo to return a valid reminder.
            mockReminderRepo.Setup(repo => repo.GetReminderById(1)).Returns(reminder);

            var service = new NoteService(mockNoteRepo.Object, mockCategoryRepo.Object, mockReminderRepo.Object);

            var actual = Assert.Throws<CategoryNotFoundException>(() => service.UpdateNote(noteId, note));
            Assert.Equal($"Category with id: {note.CategoryId} does not exist", actual.Message);

            // Rest of your test cases...
        }

        #endregion negativetests
    }
}
