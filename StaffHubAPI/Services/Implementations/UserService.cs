using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.DTOs;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<User> GetUsers()
        {
            return _unitOfWork.UserObj.GetUsers(); 
        }

        public ICollection<User> GetActiveUsers()
        {
            return _unitOfWork.UserObj.GetUsers().Where(u => u.Status).ToList();
        }

        public User GetUser(int id)
        {
            return _unitOfWork.UserObj.GetUser(id);
        }

        public bool CreateUser(User user)
        {
            return _unitOfWork.UserObj.CreateUser(user);
        }

        public bool UpdateUser(User user)
        {
            return _unitOfWork.UserObj.UpdateUser(user);
        }

        public bool DeleteUser(User user)
        {
            return _unitOfWork.UserObj.DeleteUser(user);
        }

        public User GetUserByUserName(string userName)
        {
            return _unitOfWork.UserObj.GetUser(userName);
        }
    }
}
