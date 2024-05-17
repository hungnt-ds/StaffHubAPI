using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.DataAccess.Repositories.Interface
{
    public interface IRoleClaimRepository
    {
        void AddClaimToRole(int roleId, int claimId);
        void RemoveClaimFromRole(int roleId, int claimId);
    }
}
