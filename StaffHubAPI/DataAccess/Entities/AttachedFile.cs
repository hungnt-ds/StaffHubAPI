using System.ComponentModel.DataAnnotations.Schema;

namespace StaffHubAPI.DataAccess.Entities
{
    public class AttachedFile
    {
        public int AttachedFileId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }

        [ForeignKey("SubmissionId")]
        public virtual Submission Submission { get; set; }
        public int SubmissionId { get; set; }

    }
}
