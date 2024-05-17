using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserObj { get; }
        IActualSalaryRepository ActualSalaryObj { get; }
        ISubmissionRepository SubmissionObj { get; }
        IAttachedFileRepository AttachedFileObj { get; }
        IClaimRepository ClaimObj { get; }
        IRoleRepository RoleObj { get; }
        IRoleClaimRepository RoleClaimObj { get; }

        void Save();
    }
}
