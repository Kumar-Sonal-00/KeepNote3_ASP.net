using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;
using Moq;
using Service;
using Xunit;
using System;

namespace Test.Service
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class ReminderServiceTest
    {
        #region positive tests
        [Fact, TestPriority(3)]
        public void CreateReminderShouldReturnReminder()
        {
            var mockRepo = new Mock<IReminderRepository>();
            Reminder reminder = new Reminder { ReminderName = "SMS", ReminderDescription = "SMS reminder", ReminderType = "notification", CreatedBy = "John" };

            mockRepo.Setup(repo => repo.CreateReminder(reminder)).Returns(reminder);
            var service = new ReminderService(mockRepo.Object);

            var actual = service.CreateReminder(reminder);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Reminder>(actual);
        }

        [Fact, TestPriority(7)]
        public void DeleteReminderShouldReturnTrue()
        {
            var mockRepo = new Mock<IReminderRepository>();
            int reminderId = 2;

            // Configure the mock repository to return true only if the reminder exists.
            mockRepo.Setup(repo => repo.GetReminderById(reminderId)).Returns(new Reminder()); // Return a non-null Reminder to indicate existence.
            mockRepo.Setup(repo => repo.DeletReminder(reminderId)).Returns(true);

            var service = new ReminderService(mockRepo.Object);

            // Perform the deletion operation and check if it returns true.
            var actual = service.DeleteReminder(reminderId);

            Assert.True(actual);
        }




        [Fact, TestPriority(1)]
        public void GetAllRemindersShouldReturnAList()
        {
            var userId = "John";
            var mockRepo = new Mock<IReminderRepository>();
            mockRepo.Setup(repo => repo.GetAllRemindersByUserId(userId)).Returns(this.GetReminders());
            var service = new ReminderService(mockRepo.Object);

            var actual = service.GetAllRemindersByUserId(userId);

            Assert.IsAssignableFrom<List<Reminder>>(actual);
            Assert.NotEmpty(actual);
        }

        [Fact, TestPriority(2)]
        public void GetReminderByIdShouldReturnAReminder()
        {
            int Id = 1;
            Reminder reminder = new Reminder {ReminderId=1, ReminderName = "Email", ReminderDescription = "Email reminder", ReminderType = "notification", CreatedBy = "John" };
            var mockRepo = new Mock<IReminderRepository>();
            mockRepo.Setup(repo => repo.GetReminderById(Id)).Returns(reminder);
            var service = new ReminderService(mockRepo.Object);

            var actual = service.GetReminderById(Id);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Reminder>(actual);
        }

        [Fact, TestPriority(5)]
        public void UpdateReminderShouldReturnTrue()
        {
            int Id = 1;
            Reminder reminder = new Reminder { ReminderId = 1, ReminderName = "Sports", CreatedBy = "John", ReminderDescription = "sports reminder", CreatedAt = new DateTime(), ReminderType = "sms" };
            var mockRepo = new Mock<IReminderRepository>();
            mockRepo.Setup(repo => repo.GetReminderById(Id)).Returns(reminder);
            mockRepo.Setup(repo => repo.UpdateReminder(Id,reminder)).Returns(true);
            var service = new ReminderService(mockRepo.Object);

            var actual = service.UpdateReminder(Id, reminder);
            Assert.True(actual);
        }
        private List<Reminder> GetReminders()
        {
            List<Reminder> reminders = new List<Reminder> {
               new Reminder {ReminderName= "Email", ReminderDescription= "Email reminder", ReminderType= "notification", CreatedBy="John" }
            };

            return reminders;
        }

        #endregion positive tests

        #region negative tests

        [Fact, TestPriority(6)]
        public void DeleteReminderShouldThrowException()
        {
            var mockRepo = new Mock<IReminderRepository>();
            int Id = 2;
            mockRepo.Setup(repo => repo.DeletReminder(Id)).Returns(false);
            var service = new ReminderService(mockRepo.Object);

            var actual = Assert.Throws<ReminderNotFoundException>(() => service.DeleteReminder(Id));

            Assert.Equal($"Reminder with id: {Id} does not exist", actual.Message);
        }
        [Fact, TestPriority(7)]
        public void GetAllRemindersShouldReturnEmptyList()
        {
            string userId = "Sam";
            var mockRepo = new Mock<IReminderRepository>();
            mockRepo.Setup(repo => repo.GetAllRemindersByUserId(userId)).Returns(new List<Reminder>());
            var service = new ReminderService(mockRepo.Object);

            var actual = service.GetAllRemindersByUserId(userId);

            Assert.IsAssignableFrom<List<Reminder>>(actual);
            Assert.Empty(actual);
        }

        [Fact, TestPriority(8)]
        public void GetReminderByIdShouldThrowException()
        {
            int Id = 2;
            Reminder reminder = null;
            var mockRepo = new Mock<IReminderRepository>();
            mockRepo.Setup(repo => repo.GetReminderById(Id)).Returns(reminder);
            var service = new ReminderService(mockRepo.Object);

            var actual = Assert.Throws<ReminderNotFoundException>(() => service.GetReminderById(Id));

            Assert.Equal($"Reminder with id: {Id} does not exist", actual.Message);
        }

        [Fact, TestPriority(9)]
        public void UpdateReminderShouldThrowException()
        {
            int Id = 2;
            Reminder reminder = new Reminder { ReminderName = "SMS", ReminderDescription = "SMS reminder", ReminderType = "notification", CreatedBy = "John" };
            Reminder _reminder = null;
            var mockRepo = new Mock<IReminderRepository>();
            mockRepo.Setup(repo => repo.GetReminderById(Id)).Returns(_reminder);
            mockRepo.Setup(repo => repo.UpdateReminder(Id,reminder)).Returns(false);
            var service = new ReminderService(mockRepo.Object);


            var actual = Assert.Throws<ReminderNotFoundException>(() => service.UpdateReminder(Id, reminder));
            Assert.Equal($"Reminder with id: {Id} does not exist", actual.Message);
        }

        #endregion negative tests
    }
}
