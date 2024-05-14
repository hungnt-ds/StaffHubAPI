namespace StaffHubAPI.DataAccess.Entities
{
    public class RoleClaim
    {
        public int RoleId { get; set; }
        public int ClaimId { get; set; }
        public Role Role { get; set; }
        public Claim Claim { get; set; }
    }
}
