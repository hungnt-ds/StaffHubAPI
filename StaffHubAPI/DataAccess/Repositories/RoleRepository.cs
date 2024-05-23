using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Set<Role>().AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                await _context.Set<Role>().AddAsync(role);
            }
            await _context.SaveChangesAsync();
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