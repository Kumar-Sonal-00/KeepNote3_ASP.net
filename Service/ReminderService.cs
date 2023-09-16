using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public Reminder CreateReminder(Reminder reminder)
        {
            // Placeholder implementation to create a reminder
            // You should implement the actual logic to save the reminder in the database
            // You may need to handle exceptions and perform validation here
            return _reminderRepository.CreateReminder(reminder);
        }

        public bool DeleteReminder(int reminderId)
        {
            // Check if the reminder exists before attempting to delete it.
            var existingReminder = _reminderRepository.GetReminderById(reminderId);

            if (existingReminder == null)
            {
                // If the reminder does not exist, throw a ReminderNotFoundException.
                throw new ReminderNotFoundException($"Reminder with id: {reminderId} does not exist");
            }

            // Continue with the reminder deletion logic here.
            // You should delete the reminder from the database or perform other actions.

            // Here, we assume that _reminderRepository.DeletReminder(reminderId) will perform the deletion.
            return _reminderRepository.DeletReminder(reminderId);
        }



        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            // Placeholder implementation to get all reminders by userId
            // You should implement the actual logic to retrieve reminders from the database
            // Return a list of Reminder objects
            return _reminderRepository.GetAllRemindersByUserId(userId);
        }

        public Reminder GetReminderById(int reminderId)
        {
            // Placeholder implementation to get a reminder by reminderId
            // You should implement the actual logic to retrieve a reminder from the database

            var reminder = _reminderRepository.GetReminderById(reminderId);

            if (reminder == null)
            {
                throw new ReminderNotFoundException($"Reminder with id: {reminderId} does not exist");
            }

            return reminder;
        }


        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            // Check if the reminder exists before attempting to update it.
            var existingReminder = _reminderRepository.GetReminderById(reminderId);

            if (existingReminder == null)
            {
                // If the reminder does not exist, throw a ReminderNotFoundException.
                throw new ReminderNotFoundException($"Reminder with id: {reminderId} does not exist");
            }

            // Continue with the reminder update logic here.
            // You should update the reminder in the database or perform other actions.

            return _reminderRepository.UpdateReminder(reminderId, reminder);
        }


        object IReminderService.GetRemindersByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
