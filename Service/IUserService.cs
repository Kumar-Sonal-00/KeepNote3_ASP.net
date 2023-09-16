using Entities;

namespace Service
{
    public interface IUserService
    {
        bool RegisterUser(User user);
        bool UpdateUser(string userId, User user);
        User GetUserById(string userId);
        bool ValidateUser(string userId, string password);
        bool DeleteUser(string userId);
        void CreateUser(User user);
        object LoginUser(string userId, string password);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
        object GetUserById(int userId);
    }
}
