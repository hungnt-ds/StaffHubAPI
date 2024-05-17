using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DataAccess.Entities
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string Heading { get; set; }
        public string SubmissionBody { get; set; }
        public DateTime SendDate { get; set; }
        public bool Status { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("SubmissionTypeId")]
        public int SubmissionTypeId { get; set; }
        public virtual SubmissionType SubmissionType { get; set; }

        public ICollection<AttachedFile> AttachedFiles { get; set; }
    }
}
