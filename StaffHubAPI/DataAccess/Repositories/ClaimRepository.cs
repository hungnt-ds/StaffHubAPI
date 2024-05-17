using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Claim claim)
        {
            _context.Claims.Add(claim);
            return Save();
        }

        public ICollection<Claim> GetAll()
        {
            return _context.Claims.ToList();
        }

        public Claim? Get(int id)
        {
            return _context.Claims.FirstOrDefault(c => c.ClaimId == id);
        }

        public bool Remove(Claim claim)
        {
            _context.Claims.Remove(claim);
            return Save();
        }

        public bool Update(Claim claim)
        {
            _context.Claims.Update(claim);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
