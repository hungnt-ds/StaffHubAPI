using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;
using StaffHubAPI.DTOs;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public bool CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        public bool DeleteUser(User user)
        {
            return _userRepository.DeleteUser(user);
        }

        //public User? AuthenticateUser(UserLoginRequestDTO login)
        //{
        //    var allUser = _userRepository.GetUsers();
        //    var user = allUser.SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);
        //    //return _userRepository.GetUsers().SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);
        //    return user;
        //}

        public User GetUser(string userName)
        {
            return _userRepository.GetUserByUserName(userName);
        }
    }
}
