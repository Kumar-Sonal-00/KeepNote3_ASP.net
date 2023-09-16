using Microsoft.AspNetCore.Mvc;
//using KeepNote.Service;
//using KeepNote.Entities;
//using KeepNote.Exceptions;
using Entities;
using Exceptions;
using Service;

namespace KeepNote.Controllers
{
    [ApiController]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note)
        {
            try
            {
                _noteService.CreateNote(note);
                return CreatedAtRoute("GetNoteById", new { id = note.NoteId }, note);
            }
            catch (NoteAlreadyExistsException)
            {
                return Conflict("Note with the same ID already exists.");
            }
            catch (ReminderNotFoundException)
            {
                return NotFound("Reminder not found.");
            }
            catch (CategoryNotFoundException)
            {
                return NotFound("Category not found.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                return Ok("Note deleted successfully.");
            }
            catch (NoteNotFoundException)
            {
                return NotFound("Note not found.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] Note note)
        {
            try
            {
                _noteService.UpdateNote(id, note);
                return Ok("Note updated successfully.");
            }
            catch (NoteNotFoundException)
            {
                return NotFound("Note not found.");
            }
            catch (ReminderNotFoundException)
            {
                return NotFound("Reminder not found.");
            }
            catch (CategoryNotFoundException)
            {
                return NotFound("Category not found.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetNotesByUserId(int userId)
        {
            var notes = _noteService.GetNotesByUserId(userId);
            return Ok(notes);
        }
    }
}


/*
 * Define a handler method which will create a specific note by reading the
 * Serialized object from request body and save the note details in a Note table
 * in the database.Handle ReminderNotFoundException and
 * CategoryNotFoundException as well. please note that the userID
 * should be taken as the createdBy for the note.This handler method should
 * return any one of the status messages basis on different situations: 1.
 * 201(CREATED) - If the note created successfully. 2. 409(CONFLICT) - If the
 * noteId conflicts with any existing user
 * 
 * This handler method should map to the URL "/api/note" using HTTP POST method
 */

/*
 * Define a handler method which will delete a note from a database.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the note deleted successfully from
 * database. 2. 404(NOT FOUND) - If the note with specified noteId is not found.
 * 
 * This handler method should map to the URL "/api/note/{id}" using HTTP Delete
 * method" where "id" should be replaced by a valid noteId without {}
 */

/*
 * Define a handler method which will update a specific note by reading the
 * Serialized object from request body and save the updated note details in a
 * note table in database handle ReminderNotFoundException,
 * NoteNotFoundException, CategoryNotFoundException as well. please note that
 * the userID should be taken as the createdBy for the note. This
 * handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the note updated successfully. 2.
 * 404(NOT FOUND) - If the note with specified noteId is not found.
 * This handler method should map to the URL "/api/note/{id}" using HTTP PUT method.
 */

/*
 * Define a handler method which will get us the notes by a userId.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the note found successfully.
 * 
 * This handler method should map to the URL "/api/note/{userId}" using HTTP GET method
 */

