namespace StaffHubAPI.DataAccess.Entities
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string ClaimName { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }

    }
}
