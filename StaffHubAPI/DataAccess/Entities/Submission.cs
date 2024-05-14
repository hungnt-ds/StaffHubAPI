namespace StaffHubAPI.DataAccess.Entities
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public User User { get; set; }
        public SubmissionType SubmissionType { get; set; }
        public ICollection<AttachedFile> AttachedFiles { get; set; }
        public ICollection<SalaryDetail> SalaryDetails { get; set; }
    }
}
