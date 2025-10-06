using Microsoft.EntityFrameworkCore;
using RestAPICV.Models;

namespace RestAPICV.Data
{
    public class RestAPICVContext : DbContext
    {
        public RestAPICVContext(DbContextOptions<RestAPICVContext> options) : base(options) { }

        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PersonalInfo
            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            });

            // Education
            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SchoolName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Degree).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.PersonalInfo)
                      .WithMany(p => p.Educations)
                      .HasForeignKey(e => e.PersonalInfoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkExperience
            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Company).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.PersonalInfo)
                      .WithMany(p => p.WorkExperiences)
                      .HasForeignKey(e => e.PersonalInfoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}