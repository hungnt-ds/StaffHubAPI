namespace StaffHubAPI.DataAccess.Entities
{
    public class SubmissionType
    {
        public int SubmissionTypeId { get; set; }
        public string SubmissionName { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
