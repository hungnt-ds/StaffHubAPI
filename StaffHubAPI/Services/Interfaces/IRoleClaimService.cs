﻿namespace StaffHubAPI.Services.Interfaces
{
    public interface IRoleClaimService
    {
        void AddClaimToRole(int roleId, int claimId);
        void RemoveClaimFromRole(int roleId, int claimId);
        public bool IsClamUsed(int claimId);
        public bool IsRoleUsed(int roleId);
    }
}
