using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Services.Interfaces;
using Claim = StaffHubAPI.DataAccess.Entities.Claim;

namespace StaffHubAPI.Services.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClaimService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateClaim(Claim claim)
        {
            return _unitOfWork.ClaimObj.Create(claim);
        }

        public ICollection<Claim> GetAllClaims()
        {
            return _unitOfWork.ClaimObj.GetAll();
        }
        public Claim GetClaim(int id)
        {
            return _unitOfWork.ClaimObj.Get(id);
        }

        public bool RemoveClaim(Claim claim)
        {
            return _unitOfWork.ClaimObj.Remove(claim);
        }

        public bool UpdateClaim(Claim claim)
        {
            return _unitOfWork.ClaimObj.Update(claim);
        }
    }
}