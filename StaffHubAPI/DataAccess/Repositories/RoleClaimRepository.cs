using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class RoleClaimRepository : IRoleClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddClaimToRole(int roleId, int claimId)
        {
            var roleClaim = new RoleClaim
            {
                ClaimId = claimId,
                RoleId = roleId,
            };
            _context.RoleClaims.Add(roleClaim);
            _context.SaveChanges();

        }

        public void RemoveClaimFromRole(int roleId, int claimId)
        {
            var roleClaim = _context.RoleClaims.FirstOrDefault(c => c.ClaimId == claimId && c.RoleId == roleId);
            if (roleClaim != null)
            {
                _context.RoleClaims.Remove(roleClaim);
                _context.SaveChanges();
            }
        }
    }
}
