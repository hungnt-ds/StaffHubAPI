using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.DataAccess.Repositories.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string userName);
        User GetRoleUser(int id);
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
