using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DTOs;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string userName);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        //public User? AuthenticateUser(UserLoginRequestDTO login);
    }
}
