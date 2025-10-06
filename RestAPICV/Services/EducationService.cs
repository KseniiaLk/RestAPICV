using RestAPICV.Data;
using RestAPICV.DTOs;
using RestAPICV.Models;
using Microsoft.EntityFrameworkCore;

namespace RestAPICV.Services
{
    public class EducationService
    {
        private readonly RestAPICVContext _context;

        public EducationService(RestAPICVContext context)
        {
            _context = context;
        }

        public async Task<List<EducationDTO>> GetAllAsync()
        {
            return await _context.Educations
                .Select(e => new EducationDTO
                {
                    Id = e.Id,
                    SchoolName = e.SchoolName,
                    Degree = e.Degree,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    PersonalInfoId = e.PersonalInfoId,
                    CreatedAt = e.CreatedAt
                }).ToListAsync();
        }

        public async Task<EducationDTO?> GetByIdAsync(int id)
        {
            var education = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null) return null;

            return new EducationDTO
            {
                Id = education.Id,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                PersonalInfoId = education.PersonalInfoId,
                CreatedAt = education.CreatedAt
            };
        }

        public async Task<EducationDTO> CreateAsync(CreateEducationDTO dto)
        {
            var education = new Education
            {
                SchoolName = dto.SchoolName,
                Degree = dto.Degree,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PersonalInfoId = dto.PersonalInfoId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return new EducationDTO
            {
                Id = education.Id,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                PersonalInfoId = education.PersonalInfoId,
                CreatedAt = education.CreatedAt
            };
        }

        public async Task<EducationDTO?> UpdateAsync(int id, UpdateEducationDTO dto)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null) return null;

            education.SchoolName = dto.SchoolName;
            education.Degree = dto.Degree;
            education.StartDate = dto.StartDate;
            education.EndDate = dto.EndDate;

            await _context.SaveChangesAsync();

            return new EducationDTO
            {
                Id = education.Id,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                PersonalInfoId = education.PersonalInfoId,
                CreatedAt = education.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null) return false;

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}