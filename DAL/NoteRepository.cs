using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class NoteRepository : INoteRepository
    {
        private readonly KeepDbContext _dbContext;

        public NoteRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // This method should be used to save a new note.
        public Note CreateNote(Note note)
        {
            if (note != null)
            {
                _dbContext.Notes.Add(note);
                _dbContext.SaveChanges();
                return note;
            }
            return null;
        }

        /* This method should be used to delete an existing note. */
        public bool DeleteNote(int noteId)
        {
            var existingNote = _dbContext.Notes.Find(noteId);
            if (existingNote != null)
            {
                _dbContext.Notes.Remove(existingNote);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        //* This method should be used to get all notes by userId.
        public List<Note> GetAllNotesByUserId(string userId)
        {
            return _dbContext.Notes.Where(note => note.CreatedBy == userId).ToList();
        }

        public object GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        // This method should be used to get a note by noteId.
        public Note GetNoteByNoteId(int noteId)
        {
            return _dbContext.Notes.Find(noteId);
        }

        // This method should be used to update an existing note.
        public bool UpdateNote(Note note)
        {
            var existingNote = _dbContext.Notes.Find(note.NoteId);
            if (existingNote != null)
            {
                existingNote.NoteTitle = note.NoteTitle;
                existingNote.NoteContent = note.NoteContent;
                existingNote.NoteStatus = note.NoteStatus;
                existingNote.CategoryId = note.CategoryId;
                existingNote.ReminderId = note.ReminderId;

                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateNote(int noteId, Note note)
        {
            throw new NotImplementedException();
        }
    }
}
