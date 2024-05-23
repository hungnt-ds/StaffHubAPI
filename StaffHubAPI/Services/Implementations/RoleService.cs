using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateRole(IEnumerable<Role> roles)
        {
            await _unitOfWork.RoleObj.AddRangeAsync(roles);

            return true;
        }
    }
}
