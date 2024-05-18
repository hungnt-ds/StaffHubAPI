using Microsoft.EntityFrameworkCore;
using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionType> SubmissionTypes { get; set; }
        public DbSet<ActualSalary> ActualSalaries { get; set; }
        public DbSet<AttachedFile> AttachedFiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleClaim>()
                    .HasKey(pc => new { pc.RoleId, pc.ClaimId });
            modelBuilder.Entity<RoleClaim>()
                    .HasOne(p => p.Role)
                    .WithMany(pc => pc.RoleClaims)
                    .HasForeignKey(p => p.RoleId);
            modelBuilder.Entity<RoleClaim>()
                    .HasOne(p => p.Claim)
                    .WithMany(pc => pc.RoleClaims)
                    .HasForeignKey(c => c.ClaimId);

            //modelBuilder.Entity<SalaryDetail>()
            //        .HasKey(po => new { po.ActualSalaryId, po.SubmissionId });
            //modelBuilder.Entity<SalaryDetail>()
            //        .HasOne(p => p.ActualSalary)
            //        .WithMany(pc => pc.SalaryDetails)
            //        .HasForeignKey(p => p.ActualSalaryId)
            //        .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<SalaryDetail>()
            //        .HasOne(p => p.Submission)
            //        .WithMany(pc => pc.SalaryDetails)
            //        .HasForeignKey(c => c.SubmissionId)
            //        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
