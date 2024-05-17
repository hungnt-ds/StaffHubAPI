using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DataAccess.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public decimal ContractSalary { get; set; }
        public int DaysOff { get; set; }
        public bool Status { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        [ForeignKey("RoleId")] 
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        public ICollection<RefreshToken>? RefreshTokens { get; set; }
        public ICollection<ActualSalary>? ActualSalaries { get; set; }
        public ICollection<Submission>? Submissions { get; set; }

    }
}
