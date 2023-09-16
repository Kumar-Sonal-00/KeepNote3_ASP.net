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
    [Route("api/reminder")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpPost]
        public IActionResult CreateReminder([FromBody] Reminder reminder)
        {
            try
            {
                _reminderService.CreateReminder(reminder);
                return CreatedAtRoute("GetReminderById", new { id = reminder.ReminderId }, reminder);
            }
            catch (ReminderAlreadyExistsException)
            {
                return Conflict("Reminder with the same ID already exists.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReminder(int id)
        {
            try
            {
                _reminderService.DeleteReminder(id);
                return Ok("Reminder deleted successfully.");
            }
            catch (ReminderNotFoundException)
            {
                return NotFound("Reminder not found.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReminder(int id, [FromBody] Reminder reminder)
        {
            try
            {
                _reminderService.UpdateReminder(id, reminder);
                return Ok("Reminder updated successfully.");
            }
            catch (ReminderNotFoundException)
            {
                return NotFound("Reminder not found.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetRemindersByUserId(int userId)
        {
            var reminders = _reminderService.GetRemindersByUserId(userId);
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public IActionResult GetReminderById(int id)
        {
            try
            {
                var reminder = _reminderService.GetReminderById(id);
                return Ok(reminder);
            }
            catch (ReminderNotFoundException)
            {
                return NotFound("Reminder not found.");
            }
        }
    }
}


/*
 * Define a handler method which will create a reminder by reading the
 * Serialized reminder object from request body and save the reminder in
 * reminder table in database. Please note that the reminderId has to be unique
 * and userID should be taken as the reminderCreatedBy for the
 * reminder. This handler method should return any one of the status messages
 * basis on different situations: 1. 201(CREATED - In case of successful
 * creation of the reminder 2. 409(CONFLICT) - In case of duplicate reminder ID
 * 
 * This handler method should map to the URL "/api/reminder" using HTTP POST
 * method".
 */

/*
 * Define a handler method which will delete a reminder from a database.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the reminder deleted successfully from
 * database. 2. 404(NOT FOUND) - If the reminder with specified reminderId is
 * not found. 
 * 
 * This handler method should map to the URL "/api/reminder/{id}" using HTTP Delete
 * method" where "id" should be replaced by a valid reminderId without {}
 */

/*
 * Define a handler method which will update a specific reminder by reading the
 * Serialized object from request body and save the updated reminder details in
 * a reminder table in database handle ReminderNotFoundException as well.
 * This handler method should return any one of the status
 * messages basis on different situations: 1. 200(OK) - If the reminder updated
 * successfully. 2. 404(NOT FOUND) - If the reminder with specified reminderId
 * is not found. 
 * 
 * This handler method should map to the URL "/api/reminder/{id}" using HTTP PUT
 * method.
 */

/*
 * Define a handler method which will get us the reminders by a userId.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the reminder found successfully.
 * 
 * This handler method should map to the URL "/api/reminder/{userId}" using HTTP GET method
 */

/*
 * Define a handler method which will show details of a specific reminder handle
 * ReminderNotFoundException as well. This handler method should return any one
 * of the status messages basis on different situations: 1. 200(OK) - If the
 * reminder found successfully. 2. 404(NOT FOUND) - If the reminder
 * with specified reminderId is not found. This handler method should map to the
 * URL "/api/reminder/{id}" using HTTP GET method where "id" should be replaced by a
 * valid reminderId without {}
 */

