using StaffHubAPI.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DTOs
{
    public class UserRegisterRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public decimal ContractSalary { get; set; } = 0;
        public int DaysOff { get; set; } = 0;
        public int RoleId { get; set; }
    }
}
