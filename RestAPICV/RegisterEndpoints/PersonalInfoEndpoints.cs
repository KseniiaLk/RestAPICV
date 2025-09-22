using Microsoft.EntityFrameworkCore;
using RestAPICV.Data;
using RestAPICV.DTOs;
using RestAPICV.Models;

namespace RestAPICV.Endpoints
{
    public static class PersonalInfoEndpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/personalinfo")
                .WithTags("Personal Info")
                .WithOpenApi();

            group.MapGet("/", GetAllPersonalInfo);
            group.MapGet("/{id}", GetPersonalInfoById);
            group.MapPost("/", CreatePersonalInfo);
            group.MapPut("/{id}", UpdatePersonalInfo);
            group.MapDelete("/{id}", DeletePersonalInfo);
        }

        private static async Task<IResult> GetAllPersonalInfo(RestAPICVContext context)
        {
            var personalInfos = await context.PersonalInfos
                .Include(p => p.Educations)
                .Include(p => p.WorkExperiences)
                .ToListAsync();

            return Results.Ok(personalInfos.Select(p => new PersonalInfoDTO
            {
                PersonId = p.PersonId,
                Name = p.Name,
                Description = p.Description,
                Email = p.Email,
                Phone = p.Phone,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Educations = p.Educations.Select(e => new EducationDTO
                {
                    EducationId = e.EducationId,
                    SchoolName = e.SchoolName,
                    Degree = e.Degree,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    PersonId = e.PersonId,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                }).ToList(),
                WorkExperiences = p.WorkExperiences.Select(w => new WorkExperienceDTO
                {
                    WorkExperienceId = w.WorkExperienceId,
                    JobTitle = w.JobTitle,
                    Company = w.Company,
                    Description = w.Description,
                    Year = w.Year,
                    PersonId = w.PersonId,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                }).ToList()
            }));
        }

        private static async Task<IResult> GetPersonalInfoById(int id, RestAPICVContext context)
        {
            var personalInfo = await context.PersonalInfos
                .Include(p => p.Educations)
                .Include(p => p.WorkExperiences)
                .FirstOrDefaultAsync(p => p.PersonId == id);

            if (personalInfo == null)
                return Results.NotFound();

            var result = new PersonalInfoDTO
            {
                PersonId = personalInfo.PersonId,
                Name = personalInfo.Name,
                Description = personalInfo.Description,
                Email = personalInfo.Email,
                Phone = personalInfo.Phone,
                CreatedAt = personalInfo.CreatedAt,
                UpdatedAt = personalInfo.UpdatedAt,
                Educations = personalInfo.Educations.Select(e => new EducationDTO
                {
                    EducationId = e.EducationId,
                    SchoolName = e.SchoolName,
                    Degree = e.Degree,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    PersonId = e.PersonId,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                }).ToList(),
                WorkExperiences = personalInfo.WorkExperiences.Select(w => new WorkExperienceDTO
                {
                    WorkExperienceId = w.WorkExperienceId,
                    JobTitle = w.JobTitle,
                    Company = w.Company,
                    Description = w.Description,
                    Year = w.Year,
                    PersonId = w.PersonId,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                }).ToList()
            };

            return Results.Ok(result);
        }

        private static async Task<IResult> CreatePersonalInfo(CreatePersonalInfoDTO dto, RestAPICVContext context)
        {
            var personalInfo = new PersonalInfo
            {
                Name = dto.Name,
                Description = dto.Description,
                Email = dto.Email,
                Phone = dto.Phone,
                CreatedAt = DateTime.UtcNow
            };

            context.PersonalInfos.Add(personalInfo);
            await context.SaveChangesAsync();

            return Results.Created($"/api/personalinfo/{personalInfo.PersonId}", personalInfo);
        }

        private static async Task<IResult> UpdatePersonalInfo(int id, UpdatePersonalInfoDTO dto, RestAPICVContext context)
        {
            var personalInfo = await context.PersonalInfos.FindAsync(id);
            if (personalInfo == null)
                return Results.NotFound();

            personalInfo.Name = dto.Name;
            personalInfo.Description = dto.Description;
            personalInfo.Email = dto.Email;
            personalInfo.Phone = dto.Phone;
            personalInfo.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Results.Ok(personalInfo);
        }

        private static async Task<IResult> DeletePersonalInfo(int id, RestAPICVContext context)
        {
            var personalInfo = await context.PersonalInfos.FindAsync(id);
            if (personalInfo == null)
                return Results.NotFound();

            context.PersonalInfos.Remove(personalInfo);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}