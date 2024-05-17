using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IAttachedFileService
    {
        ICollection<AttachedFile> GetAttachedFiles(); // Use AttachedFile instead of Submission
        AttachedFile GetAttachedFile(int id); // Use AttachedFile instead of Submission
        bool AttachedFileExists(int attachedFileId); // Rename to AttachedFileExists
        bool CreateAttachedFile(AttachedFile attachedFile); // Use AttachedFile instead of Submission
        bool UpdateAttachedFile(AttachedFile attachedFile); // Use AttachedFile instead of Submission
        bool DeleteAttachedFile(AttachedFile attachedFile); // Use AttachedFile instead of Submission
        bool DownloadAttachedFile(AttachedFile attachedFile); // Use AttachedFile instead of Submission
                                                              //Task DownloadFileById(int id);
                                                              //Task<Stream> DownloadFileById(int Id);
        public void DownloadFileById(int id);
        bool Save();

    }
}
