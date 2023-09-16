using System;
using System.Linq;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class UserRepository : IUserRepository
    {
        private readonly KeepDbContext _dbContext;

        public UserRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // This method should be used to delete an existing user. 
        public bool DeleteUser(string userId)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(user => user.UserId == userId);
            if (existingUser != null)
            {
                _dbContext.Users.Remove(existingUser);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        // This method should be used to get a user by userId.
        public User GetUserById(string userId)
        {
            return _dbContext.Users.FirstOrDefault(user => user.UserId == userId);
        }

        // This method should be used to save a new user.
        public bool RegisterUser(User user)
        {
            if (user != null)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        // This method should be used to update an existing user.
        public bool UpdateUser(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.Contact = user.Contact;

                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateUser(string userId, User user)
        {
            throw new NotImplementedException();
        }

        // This method should be used to validate a user using userId and password.
        public bool ValidateUser(string userId, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId && u.Password == password);
            return user != null;
        }
    }
}
