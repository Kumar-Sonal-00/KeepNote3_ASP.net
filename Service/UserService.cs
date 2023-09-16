using DAL;
using Entities;
using Exceptions;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public void CreateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteUser(string userId)
        {
            // Placeholder implementation to delete a user
            // You should implement the actual logic to delete the user from the database
            // Return true if deletion is successful, otherwise return false
            return _userRepository.DeleteUser(userId);
        }

        public void DeleteUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserById(string userId)
        {
            // Placeholder implementation to get a user by userId
            // You should implement the actual logic to retrieve a user from the database

            var user = _userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new UserNotFoundException($"User with id: {userId} does not exist");
            }

            return user;
        }


        public object GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public object LoginUser(string userId, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool RegisterUser(User user)
        {
            // Placeholder implementation to register a new user
            // You should implement the actual logic to check if the user already exists
            // and return false if the user already exists.

            var existingUser = _userRepository.GetUserById(user.UserId);

            if (existingUser != null)
            {
                throw new UserAlreadyExistException($"This userid: {user.UserId} already exists");
            }

            // Continue with the user registration logic here.
            // You should save the user in the database or perform other actions.

            return _userRepository.RegisterUser(user);
        }


        public bool UpdateUser(string userId, User user)
        {
            // Placeholder implementation to update an existing user
            // You should implement the actual logic to check if the user exists
            // and return false if the user does not exist.

            var existingUser = _userRepository.GetUserById(userId);

            if (existingUser == null)
            {
                throw new UserNotFoundException($"User with id: {userId} does not exist");
            }

            // Continue with the user update logic here.
            // You should update the user in the database or perform other actions.

            return _userRepository.UpdateUser(userId, user);
        }


        public void UpdateUser(int id, User user)
        {
            throw new System.NotImplementedException();
        }

        public bool ValidateUser(string userId, string password)
        {
            // Placeholder implementation to validate a user using userId and password
            // You should implement the actual logic to validate the user in the database
            // Return true if validation is successful, otherwise return false
            return _userRepository.ValidateUser(userId, password);
        }
    }
}
