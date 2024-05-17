using Microsoft.EntityFrameworkCore;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;
using StaffHubAPI.DataAccess.UnitOfWork;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Role? Get(int roleId)
        {
            return _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }
    }
}