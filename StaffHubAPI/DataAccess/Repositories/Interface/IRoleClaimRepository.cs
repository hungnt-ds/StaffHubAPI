namespace StaffHubAPI.DataAccess.Repositories.Interface
{
    public interface IRoleClaimRepository
    {
        void AddClaimToRole(int roleId, int claimId);
        void RemoveClaimFromRole(int roleId, int claimId);
        public bool IsClaimUsed(int claimId);
        public bool IsRoleUsed(int roleId);
    }
}
