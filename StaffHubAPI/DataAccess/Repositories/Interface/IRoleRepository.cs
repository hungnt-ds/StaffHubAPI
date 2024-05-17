using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.DataAccess.Repositories.Interface
{
    public interface IRoleRepository
    {
        Role? Get(int roleId);
        IEnumerable<Role> GetAll();
    }
}
