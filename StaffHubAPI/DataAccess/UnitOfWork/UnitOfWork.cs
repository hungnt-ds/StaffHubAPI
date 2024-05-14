using StaffHubAPI.DataAccess.Repositories;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.UnitOfWork
{
    public class UnitOfWork
    {
        //Eg public ICategoryRepository CategoryRepository {get;private set;}
        public IUserRepository UserObj { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            UserObj = new UserRepository(_db);
            

            //Eg Category = new CategoryRepository(_db);
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
