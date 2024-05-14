using Microsoft.VisualBasic.FileIO;

namespace StaffHubAPI.DataAccess.Entities
{
    public class AttachedFile
    {
        public int AttachedFileId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }

        public Submission Submission { get; set; }

    }
}
