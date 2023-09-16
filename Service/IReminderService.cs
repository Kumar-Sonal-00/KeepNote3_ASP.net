using System.Collections.Generic;
using Entities;

namespace Service
{
    public interface IReminderService
    {
        Reminder CreateReminder(Reminder reminder);
        bool UpdateReminder(int reminderId, Reminder reminder);
        bool DeleteReminder(int reminderId);
        Reminder GetReminderById(int reminderId);
        List<Reminder> GetAllRemindersByUserId(string userId);
        object GetRemindersByUserId(int userId);
    }
}
