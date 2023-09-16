using System.Collections.Generic;
using DAL;
using Entities;
using Xunit;

namespace Test.Repository
{
    [Collection("Database collection")]
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class NoteRepositoryTest
    {
        NoteRepository repository;
        public NoteRepositoryTest(DatabaseFixture databaseFixture)
        {
            repository = new NoteRepository(databaseFixture.context);
        }

        [Fact, TestPriority(4)]
        public void CreateNoteShouldSuccess()
        {
            Note note = new Note { CategoryId = 1, ReminderId = 1, NoteTitle = "Tech-Stack", NoteContent = "DotNet", NoteStatus = "Started", CreatedBy = "John" };

            var actual = repository.CreateNote(note);
            Assert.IsAssignableFrom<Note>(actual);
            Assert.Equal(2, actual.NoteId);
        }
        [Fact, TestPriority(5)]
        public void DeleteNoteShouldSuccess()
        {
            int noteId = 2;

            var actual = repository.DeleteNote(noteId);
            Assert.True(actual);
            Assert.Null(repository.GetNoteByNoteId(noteId));
        }

        [Fact, TestPriority(1)]
        public void GetAllNotesByUserIdShouldReturnList()
        {
            string userId = "John";

            var actual = repository.GetAllNotesByUserId(userId);
            Assert.IsAssignableFrom<IEnumerable<Note>>(actual);
            Assert.NotNull(actual);
        }

        [Fact, TestPriority(2)]
        public void GetNoteByNoteIdShouldReturnNote()
        {
            int noteId = 1;

            var actual = repository.GetNoteByNoteId(noteId);
            Assert.IsAssignableFrom<Note>(actual);
            Assert.Equal("Technology", actual.NoteTitle);
        }

        [Fact, TestPriority(3)]
        public void UpdateNoteShouldSuccess()
        {
            int noteId = 1;
            Note note = repository.GetNoteByNoteId(noteId);
            note.NoteStatus = "Completed";

            var actual = repository.UpdateNote(note);
            Assert.True(actual);
        }
    }
}
