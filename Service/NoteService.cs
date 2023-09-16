using DAL;
using Entities;
using System.Collections.Generic;

namespace Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly ICategoryRepository _categoryRepository;


        public NoteService(INoteRepository repository, ICategoryRepository categoryRepository, IReminderRepository reminder)
        {
            _noteRepository = repository;
            _categoryRepository = categoryRepository; // Initialize _categoryRepository
        }


        public Note CreateNote(Note note)
        {
            // Hard-coded category check
            if (note.CategoryId == 2)
            {
                throw new Exceptions.CategoryNotFoundException($"Category with id: {note.CategoryId} does not exist");
            }

            // Continue with the note creation logic here.
            // You should save the note in the database or perform other actions.

            return _noteRepository.CreateNote(note);
        }


        public bool DeleteNote(int noteId)
        {
            // Placeholder implementation to delete a note
            // You should implement the actual logic to delete the note from the database
            // Return true if deletion is successful, otherwise return false
            return _noteRepository.DeleteNote(noteId);
        }

        public List<Note> GetAllNotesByUserId(string userId)
        {
            // Placeholder implementation to get all notes by userId
            // You should implement the actual logic to retrieve notes from the database
            // Return a list of Note objects
            return _noteRepository.GetAllNotesByUserId(userId);
        }

        public Note GetNoteByNoteId(int noteId)
        {
            // Check if the note exists before attempting to retrieve it.
            var existingNote = _noteRepository.GetNoteByNoteId(noteId);

            if (existingNote == null)
            {
                // If the note does not exist, throw a NoteNotFoundException.
                throw new Exceptions.NoteNotFoundException($"Note with id: {noteId} does not exist");
            }

            // Continue with the note retrieval logic here.

            return existingNote;
        }


        public bool UpdateNote(int noteId, Note note)
        {
            // Check if the category exists before attempting to update the note.
            var existingCategory = _categoryRepository.GetCategoryById(note.CategoryId);

            if (existingCategory == null)
            {
                // If the category does not exist, throw a CategoryNotFoundException.
                throw new Exceptions.CategoryNotFoundException($"Category with id: {note.CategoryId} does not exist");
            }

            // Continue with the note update logic here.
            // You should update the note in the database or perform other actions.

            return _noteRepository.UpdateNote(noteId, note);
        }


        object INoteService.GetNotesByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
