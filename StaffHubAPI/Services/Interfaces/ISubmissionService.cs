using StaffHubAPI.DataAccess.DTOs;
using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface ISubmissionService
    {
        Submission CreateSubmission(SubmissionDTO dto, IFormFile fileData, int userId);
        Submission GetSubmissionById(int id);
        bool UpdateSubmission(Submission submission);
    }
}
