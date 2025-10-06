using RestAPICV.Data;
using RestAPICV.DTOs;
using RestAPICV.Models;
using Microsoft.EntityFrameworkCore;

namespace RestAPICV.Services
{
    public class PersonalInfoService
    {
        private readonly RestAPICVContext _context;

        public PersonalInfoService(RestAPICVContext context)
        {
            _context = context;
        }

        public async Task<List<PersonalInfoDTO>> GetAllAsync()
        {
            return await _context.PersonalInfos
                .Include(p => p.Educations)
                .Include(p => p.WorkExperiences)
                .Select(p => new PersonalInfoDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Email = p.Email,
                    Phone = p.Phone,
                    CreatedAt = p.CreatedAt,
                    Educations = p.Educations.Select(e => new EducationDTO
                    {
                        Id = e.Id,
                        SchoolName = e.SchoolName,
                        Degree = e.Degree,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        PersonalInfoId = e.PersonalInfoId,
                        CreatedAt = e.CreatedAt
                    }).ToList(),
                    WorkExperiences = p.WorkExperiences.Select(w => new WorkExperienceDTO
                    {
                        Id = w.Id,
                        JobTitle = w.JobTitle,
                        Company = w.Company,
                        Description = w.Description,
                        Year = w.Year,
                        PersonalInfoId = w.PersonalInfoId,
                        CreatedAt = w.CreatedAt
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<PersonalInfoDTO?> GetByIdAsync(int id)
        {
            var personalInfo = await _context.PersonalInfos
                .Include(p => p.Educations)
                .Include(p => p.WorkExperiences)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (personalInfo == null) return null;

            return new PersonalInfoDTO
            {
                Id = personalInfo.Id,
                Name = personalInfo.Name,
                Description = personalInfo.Description,
                Email = personalInfo.Email,
                Phone = personalInfo.Phone,
                CreatedAt = personalInfo.CreatedAt,
                Educations = personalInfo.Educations.Select(e => new EducationDTO
                {
                    Id = e.Id,
                    SchoolName = e.SchoolName,
                    Degree = e.Degree,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    PersonalInfoId = e.PersonalInfoId,
                    CreatedAt = e.CreatedAt
                }).ToList(),
                WorkExperiences = personalInfo.WorkExperiences.Select(w => new WorkExperienceDTO
                {
                    Id = w.Id,
                    JobTitle = w.JobTitle,
                    Company = w.Company,
                    Description = w.Description,
                    Year = w.Year,
                    PersonalInfoId = w.PersonalInfoId,
                    CreatedAt = w.CreatedAt
                }).ToList()
            };
        }

        public async Task<PersonalInfoDTO> CreateAsync(CreatePersonalInfoDTO dto)
        {
            var personalInfo = new PersonalInfo
            {
                Name = dto.Name,
                Description = dto.Description,
                Email = dto.Email,
                Phone = dto.Phone,
                CreatedAt = DateTime.UtcNow
            };

            _context.PersonalInfos.Add(personalInfo);
            await _context.SaveChangesAsync();

            return new PersonalInfoDTO
            {
                Id = personalInfo.Id,
                Name = personalInfo.Name,
                Description = personalInfo.Description,
                Email = personalInfo.Email,
                Phone = personalInfo.Phone,
                CreatedAt = personalInfo.CreatedAt
            };
        }

        public async Task<PersonalInfoDTO?> UpdateAsync(int id, UpdatePersonalInfoDTO dto)
        {
            var personalInfo = await _context.PersonalInfos.FindAsync(id);
            if (personalInfo == null) return null;

            personalInfo.Name = dto.Name;
            personalInfo.Description = dto.Description;
            personalInfo.Email = dto.Email;
            personalInfo.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return new PersonalInfoDTO
            {
                Id = personalInfo.Id,
                Name = personalInfo.Name,
                Description = personalInfo.Description,
                Email = personalInfo.Email,
                Phone = personalInfo.Phone,
                CreatedAt = personalInfo.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personalInfo = await _context.PersonalInfos.FindAsync(id);
            if (personalInfo == null) return false;

            _context.PersonalInfos.Remove(personalInfo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}