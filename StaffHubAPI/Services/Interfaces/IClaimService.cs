using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IClaimService
    {
        bool CreateClaim(Claim claim);
        ICollection<Claim> GetAllClaims();
        Claim GetClaim(int id);
        bool RemoveClaim(Claim claim);
        bool UpdateClaim(Claim claim);
    }
}
