using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DataAccess.Entities
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string RefToken { get; set; }
        public DateTime ExpierTime { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("UserId")] public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
