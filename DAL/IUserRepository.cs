using Entities;

namespace DAL
{
    /*
	 * Should not modify this interface. You have to implement these methods in
	 * corresponding Impl classes
	 */

    public interface IUserRepository
    {
        bool RegisterUser(User user);
        bool UpdateUser(string userId, User user);
        User GetUserById(string userId);
        bool ValidateUser(string userId, string password);
        bool DeleteUser(string userId);
    }
}
