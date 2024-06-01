using StaffHubAPI.DataAccess.Repositories;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //Eg public ICategoryRepository CategoryRepository {get;private set;}
        public IUserRepository UserObj { get; private set; }
        public IActualSalaryRepository ActualSalaryObj { get; private set; }
        public ISubmissionRepository SubmissionObj { get; private set; }
        public IAttachedFileRepository AttachedFileObj { get; private set; }
        public IClaimRepository ClaimObj { get; private set; }
        public IRoleRepository RoleObj { get; private set; }
        public IRoleClaimRepository RoleClaimObj { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            UserObj = new UserRepository(_db);
            ActualSalaryObj = new ActualSalaryRepository(_db);
            SubmissionObj = new SubmissionRepository(_db);
            AttachedFileObj = new AttachedFileRepository(_db);
            ClaimObj = new ClaimRepository(_db);
            RoleObj = new RoleRepository(_db);
            RoleClaimObj = new RoleClaimRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
