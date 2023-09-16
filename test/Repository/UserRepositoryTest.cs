using DAL;
using Entities;
using Xunit;

namespace Test.Repository
{
    [Collection("Database collection")]
    [TestCaseOrderer("Test.PriorityOrderer","Test")]
    public class UserRepositoryTest
    {
        UserRepository repository;
        public UserRepositoryTest(DatabaseFixture fixture)
        {
            repository = new UserRepository(fixture.context);
        }

        [Fact, TestPriority(4)]
        public void RegisterUserShouldSuccess()
        {
            User user = new User {UserId="Sam", UserName="Sam Gomes", Password="test123", Contact="9876543210" };

            var actual = repository.RegisterUser(user);
            Assert.True(actual);
        }
        [Fact, TestPriority(5)]
        public void DeleteUserShouldSuccess()
        {
            string userId = "Sam";

            var actual = repository.DeleteUser(userId);
            Assert.True(actual);
        }

        [Fact,TestPriority(3)]
        public void UpdateUserShouldSuccess()
        {
            var user = repository.GetUserById("John");
            user.Password = "admin123";

            var actual = repository.UpdateUser(user);
            Assert.True(actual);
        }

        [Fact, TestPriority(2)]
        public void GetUserByIdShouldSuccess()
        {
            var user = repository.GetUserById("John");

            Assert.IsAssignableFrom<User>(user);
            Assert.Equal("John Simon", user.UserName);
        }

        [Fact, TestPriority(1)]
        public void ValidateUserShouldSuccess()
        {
            string userId = "John";
            string password = "test123";
            var actual = repository.ValidateUser(userId,password);

            Assert.True(actual);
        }
    }
}
