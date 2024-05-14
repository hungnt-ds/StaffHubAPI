using System.ComponentModel.DataAnnotations;

namespace StaffHubAPI.DataAccess.Entities
{
    public class ActualSalary
    {
        public int ActualSalaryId { get; set; }
        public decimal SalaryAfterDeductions { get; set; }
        public int Month { get; set; }
        public int DaysOff { get; set; }
        public User User { get; set; }
        public ICollection<SalaryDetail> SalaryDetails { get; set; }
    }
}
