using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRole(IEnumerable<Role> roles);
    }
}
