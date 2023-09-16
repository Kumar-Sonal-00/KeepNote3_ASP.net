using System.Collections.Generic;
using Entities;

namespace Service
{
    public interface INoteService
    {
        Note CreateNote(Note note);
        bool UpdateNote(int noteId, Note note);
        bool DeleteNote(int noteId);
        Note GetNoteByNoteId(int noteId);
        List<Note> GetAllNotesByUserId(string userId);
        object GetNotesByUserId(int userId);
    }
}
