using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Data
{
    public class ZonartUsersDbContext : IdentityDbContext<User>
    {
        public ZonartUsersDbContext(DbContextOptions<ZonartUsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Template> Templates { get; set; }



        public DbSet<Recruiter> Recruiters { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<CandidateSkill> CandidatesSkills { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<JobSkill> JobsSkills { get; set; }

        public DbSet<Interview> Interviews { get; set; }

        public DbSet<Skill> Skills { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Candidate>()
                .HasOne(c => c.Recruiter)
                .WithMany(r => r.Candidates)
                .HasForeignKey(r => r.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
