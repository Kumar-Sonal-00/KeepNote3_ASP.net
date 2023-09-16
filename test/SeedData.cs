using Entities;
using DAL;

namespace Test
{
    public static class SeedData
    {
        public static void PopulateTestData(KeepDbContext context)
        {
            AddUsers(context);
            AddCategories(context);
            AddReminders(context);
            AddNotes(context);
        }

        private static void AddCategories(KeepDbContext context)
        {
            context.Categories.Add(new Category { CategoryName = "Testing", CategoryDescription = "Unit Testing", CategoryCreatedBy = "John" });
            context.SaveChanges();
        }
        private static void AddReminders(KeepDbContext context)
        {
            context.Reminders.Add(new Reminder { ReminderName = "Email", ReminderDescription = "Email reminder", ReminderType = "notification", CreatedBy = "John" });
            context.SaveChanges();
        }
        private static void AddUsers(KeepDbContext context)
        {
            context.Users.Add(new User { UserId = "John", UserName = "John Simon", Password = "test123", Contact = "9812345670" });
            context.SaveChanges();
        }
        private static void AddNotes(KeepDbContext context)
        {
            context.Notes.Add(new Note { CategoryId = 1, ReminderId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Started", CreatedBy = "John" });
            context.SaveChanges();
        }

    }
}
