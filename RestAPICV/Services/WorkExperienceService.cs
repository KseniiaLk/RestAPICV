using RestAPICV.Data;
using RestAPICV.DTOs;
using RestAPICV.Models;
using Microsoft.EntityFrameworkCore;

namespace RestAPICV.Services
{
    public class WorkExperienceService
    {
        private readonly RestAPICVContext _context;

        public WorkExperienceService(RestAPICVContext context)
        {
            _context = context;
        }

        public async Task<List<WorkExperienceDTO>> GetAllAsync()
        {
            return await _context.WorkExperiences
                .Select(w => new WorkExperienceDTO
                {
                    Id = w.Id,
                    JobTitle = w.JobTitle,
                    Company = w.Company,
                    Description = w.Description,
                    Year = w.Year,
                    PersonalInfoId = w.PersonalInfoId,
                    CreatedAt = w.CreatedAt
                }).ToListAsync();
        }

        public async Task<WorkExperienceDTO?> GetByIdAsync(int id)
        {
            var workExperience = await _context.WorkExperiences.FirstOrDefaultAsync(w => w.Id == id);
            if (workExperience == null) return null;

            return new WorkExperienceDTO
            {
                Id = workExperience.Id,
                JobTitle = workExperience.JobTitle,
                Company = workExperience.Company,
                Description = workExperience.Description,
                Year = workExperience.Year,
                PersonalInfoId = workExperience.PersonalInfoId,
                CreatedAt = workExperience.CreatedAt
            };
        }

        public async Task<WorkExperienceDTO> CreateAsync(CreateWorkExperienceDTO dto)
        {
            var workExperience = new WorkExperience
            {
                JobTitle = dto.JobTitle,
                Company = dto.Company,
                Description = dto.Description,
                Year = dto.Year,
                PersonalInfoId = dto.PersonalInfoId,
                CreatedAt = DateTime.UtcNow
            };

            _context.WorkExperiences.Add(workExperience);
            await _context.SaveChangesAsync();

            return new WorkExperienceDTO
            {
                Id = workExperience.Id,
                JobTitle = workExperience.JobTitle,
                Company = workExperience.Company,
                Description = workExperience.Description,
                Year = workExperience.Year,
                PersonalInfoId = workExperience.PersonalInfoId,
                CreatedAt = workExperience.CreatedAt
            };
        }

        public async Task<WorkExperienceDTO?> UpdateAsync(int id, UpdateWorkExperienceDTO dto)
        {
            var workExperience = await _context.WorkExperiences.FindAsync(id);
            if (workExperience == null) return null;

            workExperience.JobTitle = dto.JobTitle;
            workExperience.Company = dto.Company;
            workExperience.Description = dto.Description;
            workExperience.Year = dto.Year;

            await _context.SaveChangesAsync();

            return new WorkExperienceDTO
            {
                Id = workExperience.Id,
                JobTitle = workExperience.JobTitle,
                Company = workExperience.Company,
                Description = workExperience.Description,
                Year = workExperience.Year,
                PersonalInfoId = workExperience.PersonalInfoId,
                CreatedAt = workExperience.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var workExperience = await _context.WorkExperiences.FindAsync(id);
            if (workExperience == null) return false;

            _context.WorkExperiences.Remove(workExperience);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}