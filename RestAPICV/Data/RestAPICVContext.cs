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
            // PersonalInfo conf
            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.HasKey(e => e.PersonId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.CreatedAt).IsRequired();
            });

            // Education conf
            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.EducationId);
                entity.Property(e => e.SchoolName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Degree).IsRequired().HasMaxLength(200);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                entity.HasOne(e => e.Person)
                      .WithMany(p => p.Educations)
                      .HasForeignKey(e => e.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkExperience conf
            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.HasKey(e => e.WorkExperienceId);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Company).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                entity.HasOne(e => e.Person)
                      .WithMany(p => p.WorkExperiences)
                      .HasForeignKey(e => e.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}