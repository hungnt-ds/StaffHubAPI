using Microsoft.AspNetCore.Mvc;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DTOs;
using UploadFileAPI.Models;

namespace StaffHubAPI.Services.Interfaces
{
    public interface ISubmissionService
    {
        Submission CreateSubmission(SubmissionDTO dto, IFormFile fileData, int userId);
        Submission GetSubmissionById(int id);
        bool UpdateSubmission(Submission submission);
    }
}
