using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Services.Interfaces;

namespace StaffHubAPI.Services.Implementations
{
    public class AttachedFileService : IAttachedFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttachedFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DownloadFileById(int id)
        {
            try
            {
                string downloadPath = "FileDownloaded";
                var file = _unitOfWork.AttachedFileObj.GetAttachedFile(id);

                if (file == null)
                {
                    throw new FileNotFoundException("File not found with the specified ID.");
                }

                // Assuming your file data is stored in a byte array
                var memoryStream = new MemoryStream(file.FileData);

                // Ensure the download directory exists
                if (!Directory.Exists(downloadPath))
                {
                    Directory.CreateDirectory(downloadPath);
                }

                // Construct the full file path
                var filePath = Path.Combine(downloadPath, file.FileName);

                // Write the file data to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw; // Re-throw for handling at a higher level
            }
        }

        public bool AttachedFileExists(int attachedFileId)
        {
            throw new NotImplementedException();
        }

        public bool CreateAttachedFile(AttachedFile attachedFile)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAttachedFile(AttachedFile attachedFile)
        {
            throw new NotImplementedException();
        }

        //public async Task DownloadFileById(int id)
        //{
        //    try
        //    {
        //        //var files = _unitOfWork.AttachedFileRepositoryObj.GetAttachedFile(id);
        //        //var file_1 = _unitOfWork.AttachedFileRepositoryObj.GetAttachedFile(id).FirstOrDefault();

        //        //var file = dbContextClass.FileDetails.Where(x => x.ID == Id).FirstOrDefaultAsync();
        //        var file = _unitOfWork.AttachedFileRepositoryObj.GetAttachedFile(id);

        //        if (file == null)
        //        {
        //            throw new FileNotFoundException("File not found with the specified ID.");
        //        }

        //        // Create memory stream from file data
        //        var content = new System.IO.MemoryStream(file.FileData);

        //        // Construct the download path
        //        var path = Path.Combine(
        //            Directory.GetCurrentDirectory(),
        //            "FileDownloaded",
        //            file.FileName);

        //        // Copy the file data asynchronously
        //        await CopyStream(content, path);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task CopyStream(Stream stream, string downloadPath)
        //{
        //    using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
        //    {
        //        await stream.CopyToAsync(fileStream);
        //    }
        //}

        //public async Task<Stream> DownloadFileById(int id)
        //{
        //    try
        //    {
        //        var file = _unitOfWork.AttachedFileRepositoryObj.GetAttachedFile(id);

        //        if (file == null)
        //        {
        //            return null; // Indicate file not found
        //        }

        //        var memoryStream = new MemoryStream(file.FileData);
        //        return memoryStream;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw; // Re-throw for controller to handle
        //    }
        //}

        public bool DownloadAttachedFile(AttachedFile attachedFile)
        {
            throw new NotImplementedException();
        }

        public AttachedFile GetAttachedFile(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AttachedFile> GetAttachedFiles()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateAttachedFile(AttachedFile attachedFile)
        {
            throw new NotImplementedException();
        }

        //public Task DownloadFileById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
