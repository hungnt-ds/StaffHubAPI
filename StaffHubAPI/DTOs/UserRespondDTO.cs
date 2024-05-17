using StaffHubAPI.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DTOs
{
    public class UserRespondDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public decimal ContractSalary { get; set; }
        public int DaysOff { get; set; }
        public int RoleId { get; set; }

    }
}
