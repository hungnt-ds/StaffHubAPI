using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class ActualSalaryRepository : IActualSalaryRepository
    {
        private readonly ApplicationDbContext _context;

        public ActualSalaryRepository(ApplicationDbContext context) {
            this._context = context;
        }
        public bool ActualSalaryExists(int actualSalaryId)
        {
            return _context.ActualSalaries.Any(a => a.ActualSalaryId == actualSalaryId);
        }

        public bool CreateActualSalary(ActualSalary actualSalary)
        {
            _context.ActualSalaries.Add(actualSalary);
            return Save(); // Call Save method to persist changes
        }

        public ActualSalary GetActualSalary(int userId)
        {
            return _context.ActualSalaries.FirstOrDefault(a => a.UserId == userId);
        }

        public ICollection<ActualSalary> GetActualSalarys()
        {
            return _context.ActualSalaries.ToList(); // Use ToList to materialize the collection
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0; // Check if any changes were saved
        }

        public bool UpdateActualSalary(ActualSalary actualSalary)
        {
            _context.ActualSalaries.Update(actualSalary);
            return Save(); // Call Save method to persist changes
        }
    }
}
