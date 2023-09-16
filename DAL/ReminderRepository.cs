using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class ReminderRepository : IReminderRepository
    {
        private readonly KeepDbContext _dbContext;

        public ReminderRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // This method should be used to save a new reminder.
        public Reminder CreateReminder(Reminder reminder)
        {
            if (reminder != null)
            {
                _dbContext.Reminders.Add(reminder);
                _dbContext.SaveChanges();
                return reminder;
            }
            return null;
        }

        // This method should be used to delete an existing reminder.
        public bool DeleteReminder(int reminderId)
        {
            var existingReminder = _dbContext.Reminders.Find(reminderId);
            if (existingReminder != null)
            {
                _dbContext.Reminders.Remove(existingReminder);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletReminder(int reminderId)
        {
            // Assuming you are using Entity Framework to manage the database
            var reminderToDelete = _dbContext.Reminders.SingleOrDefault(r => r.ReminderId == reminderId);

            if (reminderToDelete != null)
            {
                _dbContext.Reminders.Remove(reminderToDelete);
                _dbContext.SaveChanges(); // Save changes to commit the deletion.
                return true;
            }

            return false; // Return false if the reminder with the specified ID was not found.
        }


        // This method should be used to get all reminders by userId.
        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return _dbContext.Reminders.Where(reminder => reminder.ReminderCreatedBy == userId).ToList();
        }

        // This method should be used to get a reminder by reminderId.
        public Reminder GetReminderById(int reminderId)
        {
            return _dbContext.Reminders.Find(reminderId);
        }

        // This method should be used to update an existing reminder.
        public bool UpdateReminder(Reminder reminder)
        {
            var existingReminder = _dbContext.Reminders.Find(reminder.ReminderId);
            if (existingReminder != null)
            {
                existingReminder.ReminderName = reminder.ReminderName;
                existingReminder.ReminderDescription = reminder.Description;
                existingReminder.ReminderType = reminder.ReminderType;

                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            throw new NotImplementedException();
        }

        void IReminderRepository.SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
