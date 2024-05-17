using AutoMapper;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.DTOs;
using StaffHubAPI.Services.Interfaces;
using UploadFileAPI.Models;

namespace StaffHubAPI.Services.Implementations
{
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachedFileRepository _attachedFileRepository;
        private readonly IMapper _mapper;

        public SubmissionService(
            IUnitOfWork unitOfWork, 
            IAttachedFileRepository attachedFileRepository, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attachedFileRepository = attachedFileRepository;
            _mapper = mapper;
        }

        public Submission CreateSubmission(SubmissionDTO dto, IFormFile fileData, int userId)
        {
            try
            {
                // Mapper here
                var submission = _mapper.Map<Submission>(dto); 
                submission.UserId = userId;
                submission.SendDate = DateTime.Now;
                submission.Status = false;

                _unitOfWork.SubmissionObj.CreateSubmission(submission);
                _unitOfWork.Save();

                var attachedFile = new AttachedFile()
                {
                    FileName = fileData.FileName,
                    SubmissionId = submission.SubmissionId 
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    attachedFile.FileData = stream.ToArray();
                }

                _attachedFileRepository.CreateAttachedFile(attachedFile);
                _attachedFileRepository.Save();

                return submission; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Submission GetSubmissionById(int id)
        {
            return _unitOfWork.SubmissionObj.GetSubmission(id);
        }

        public bool UpdateSubmission(Submission submission)
        {
            return _unitOfWork.SubmissionObj.UpdateSubmission(submission);
        }
    }
}
