using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DataAccess.Entities
{
    public class ActualSalary
    {
        public int ActualSalaryId { get; set; }
        public decimal ContractSalary { get; set; }
        public decimal SalaryAfterDeductions { get; set; } 
        public int Month { get; set; }
        public int Year { get; set; }
        public int DaysOff { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
