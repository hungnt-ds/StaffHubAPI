using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class AttachedFileRepository : IAttachedFileRepository
    {
        private readonly ApplicationDbContext _context;

        public AttachedFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<AttachedFile> GetAttachedFiles()
        {
            return _context.AttachedFiles.ToList();
        }

        public AttachedFile GetAttachedFile(int id)
        {
            return _context.AttachedFiles.FirstOrDefault(u => u.AttachedFileId == id);
        }

        public bool AttachedFileExists(int attachedFileId)
        {
            return _context.AttachedFiles.Any(u => u.AttachedFileId == attachedFileId);
        }

        public bool CreateAttachedFile(AttachedFile attachedFile)
        {
            _context.AttachedFiles.Add(attachedFile);
            return Save();
        }

        public bool UpdateAttachedFile(AttachedFile attachedFile)
        {
            _context.AttachedFiles.Update(attachedFile);
            return Save();
        }

        public bool DeleteAttachedFile(AttachedFile attachedFile)
        {
            _context.AttachedFiles.Remove(attachedFile);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}
