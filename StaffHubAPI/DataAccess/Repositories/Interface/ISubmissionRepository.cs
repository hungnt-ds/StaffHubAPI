using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.DataAccess.Repositories.Interface
{
    public interface ISubmissionRepository
    {

        ICollection<Submission> GetSubmissions();
        Submission GetSubmission(int id);
        bool SubmissionExists(int submissionId);
        bool CreateSubmission(Submission submission);
        bool UpdateSubmission(Submission submission);
        bool DeleteSubmission(Submission submission);
        bool Save();
        Submission GetSubmissionByTitle(string title);

    }
}
