using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
        ICollection<User> GetActiveUsers();
        User GetUser(int id);
        User GetUserByUserName(string userName);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        //public User? AuthenticateUser(UserLoginRequestDTO login);
    }
}
