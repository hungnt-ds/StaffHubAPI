using Microsoft.EntityFrameworkCore;
using StaffHubAPI.DataAccess;

namespace StaffHubAPI.Helper.Middleware
{
    public class UserClaimService
    {
        private readonly ApplicationDbContext _context;

        public UserClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetUserClaimsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.RoleClaims)
                        .ThenInclude(rc => rc.Claim)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return new List<string>();
            }

            return user.Role.RoleClaims.Select(rc => rc.Claim.ClaimName).ToList();
        }
    }

}
