using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DataAccess.Repositories.Interface;

namespace StaffHubAPI.DataAccess.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Submission> GetSubmissions()
        {
            return _context.Submissions.ToList();
        }

        public Submission GetSubmission(int id)
        {
            return _context.Submissions.FirstOrDefault(u => u.SubmissionId == id);
        }

        public bool SubmissionExists(int submissionId)
        {
            return _context.Submissions.Any(u => u.SubmissionId == submissionId);
        }

        public bool CreateSubmission(Submission submission)
        {
            _context.Submissions.Add(submission);
            return Save();
        }

        public bool UpdateSubmission(Submission submission)
        {
            _context.Submissions.Update(submission);
            return Save();
        }

        public bool DeleteSubmission(Submission submission)
        {
            _context.Submissions.Remove(submission);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public Submission GetSubmissionByTitle(string title)
        {
            return _context.Submissions.FirstOrDefault(u => u.Heading == title);
        }
    }
}
