using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Services.Implementations
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleClaimService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddClaimToRole(int roleId, int claimId)
        {
            var role = _unitOfWork.RoleObj.Get(roleId);
            if (role == null)
            {
                throw new ArgumentException($"Role with ID {roleId} does not exist.");
            }

            var claim = _unitOfWork.ClaimObj.Get(claimId);
            if (claim == null)
            {
                throw new ArgumentException($"Claim with ID {claimId} does not exist.");
            }
            _unitOfWork.RoleClaimObj.AddClaimToRole(roleId, claimId);
        }

        public void RemoveClaimFromRole(int roleId, int claimId)
        {
            var role = _unitOfWork.RoleObj.Get(roleId);
            if (role == null)
            {
                throw new ArgumentException($"Role with ID {roleId} does not exist.");
            }

            var claim = _unitOfWork.ClaimObj.Get(claimId);
            if (claim == null)
            {
                throw new ArgumentException($"Claim with ID {claimId} does not exist.");
            }
            _unitOfWork.RoleClaimObj.RemoveClaimFromRole(roleId, claimId);
        }

        public bool IsRoleUsed(int roleId)
        {
            return _unitOfWork.RoleClaimObj.IsRoleUsed(roleId);
        }

        public bool IsClamUsed(int claimId)
        {
            return _unitOfWork.RoleClaimObj.IsClaimUsed(claimId);
        }
    }
}
