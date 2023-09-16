using System.Collections.Generic;
using DAL;
using Entities;
using Xunit;

namespace Test.Repository
{
    [Collection("Database collection")]
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class ReminderRepositoryTest
    {
        ReminderRepository repository;
        public ReminderRepositoryTest(DatabaseFixture databaseFixture)
        {
            repository = new ReminderRepository(databaseFixture.context);
        }

        [Fact, TestPriority(4)]
        public void CreateReminderShouldSuccess()
        {
            Reminder reminder = new Reminder { ReminderName = "SMS", ReminderDescription = "SMS reminder", ReminderType = "notification", CreatedBy = "John" };

            var actual = repository.CreateReminder(reminder);
            Assert.IsAssignableFrom<Reminder>(actual);
            Assert.Equal(2, actual.ReminderId);
        }

        [Fact, TestPriority(5)]
        public void DeletReminderShouldSuccess()
        {
            int reminderId = 2;

            var actual = repository.DeletReminder(reminderId);
            Assert.True(actual);
            Assert.Null(repository.GetReminderById(reminderId));
        }
        [Fact, TestPriority(1)]
        public void GetAllRemindersByUserIdShouldReturnList()
        {
            string userId = "John";

            var actual = repository.GetAllRemindersByUserId(userId);
            Assert.IsAssignableFrom<IEnumerable<Reminder>>(actual);
            Assert.NotNull(actual);
        }

        [Fact, TestPriority(2)]
        public void GetReminderByIdShouldReturnReminder()
        {
            int reminderId = 1;

            var actual = repository.GetReminderById(reminderId);
            Assert.IsAssignableFrom<Reminder>(actual);
            Assert.Equal("Email",actual.ReminderName);
        }

        [Fact, TestPriority(3)]
        public void UpdateReminderShouldSuccess()
        {
            int reminderId = 1;
            Reminder reminder= repository.GetReminderById(reminderId);
            reminder.ReminderName = "Email Test";

            var actual = repository.UpdateReminder(reminder);
            Assert.True(actual);
        }
    }
}
