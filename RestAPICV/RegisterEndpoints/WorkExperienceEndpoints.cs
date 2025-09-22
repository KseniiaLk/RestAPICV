using Microsoft.EntityFrameworkCore;
using RestAPICV.Data;
using RestAPICV.DTOs;
using RestAPICV.Models;

namespace RestAPICV.Endpoints
{
    public static class WorkExperienceEndpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/workexperience")
                .WithTags("Work Experience")
                .WithOpenApi();

            group.MapGet("/", GetAllWorkExperience);
            group.MapGet("/{id}", GetWorkExperienceById);
            group.MapPost("/", CreateWorkExperience);
            group.MapPut("/{id}", UpdateWorkExperience);
            group.MapDelete("/{id}", DeleteWorkExperience);
        }

        private static async Task<IResult> GetAllWorkExperience(RestAPICVContext context)
        {
            var workExperiences = await context.WorkExperiences.ToListAsync();
            return Results.Ok(workExperiences.Select(w => new WorkExperienceDTO
            {
                WorkExperienceId = w.WorkExperienceId,
                JobTitle = w.JobTitle,
                Company = w.Company,
                Description = w.Description,
                Year = w.Year,
                PersonId = w.PersonId,
                CreatedAt = w.CreatedAt,
                UpdatedAt = w.UpdatedAt
            }));
        }

        private static async Task<IResult> GetWorkExperienceById(int id, RestAPICVContext context)
        {
            var workExperience = await context.WorkExperiences.FindAsync(id);
            if (workExperience == null)
                return Results.NotFound();

            var result = new WorkExperienceDTO
            {
                WorkExperienceId = workExperience.WorkExperienceId,
                JobTitle = workExperience.JobTitle,
                Company = workExperience.Company,
                Description = workExperience.Description,
                Year = workExperience.Year,
                PersonId = workExperience.PersonId,
                CreatedAt = workExperience.CreatedAt,
                UpdatedAt = workExperience.UpdatedAt
            };

            return Results.Ok(result);
        }

        private static async Task<IResult> CreateWorkExperience(CreateWorkExperienceDTO dto, RestAPICVContext context)
        {
            var person = await context.PersonalInfos.FindAsync(dto.PersonId);
            if (person == null)
                return Results.BadRequest();

            var workExperience = new WorkExperience
            {
                JobTitle = dto.JobTitle,
                Company = dto.Company,
                Description = dto.Description,
                Year = dto.Year,
                PersonId = dto.PersonId,
                CreatedAt = DateTime.UtcNow
            };

            context.WorkExperiences.Add(workExperience);
            await context.SaveChangesAsync();

            return Results.Created($"/api/workexperience/{workExperience.WorkExperienceId}", workExperience);
        }

        private static async Task<IResult> UpdateWorkExperience(int id, UpdateWorkExperienceDTO dto, RestAPICVContext context)
        {
            var workExperience = await context.WorkExperiences.FindAsync(id);
            if (workExperience == null)
                return Results.NotFound();

            workExperience.JobTitle = dto.JobTitle;
            workExperience.Company = dto.Company;
            workExperience.Description = dto.Description;
            workExperience.Year = dto.Year;
            workExperience.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Results.Ok(workExperience);
        }

        private static async Task<IResult> DeleteWorkExperience(int id, RestAPICVContext context)
        {
            var workExperience = await context.WorkExperiences.FindAsync(id);
            if (workExperience == null)
                return Results.NotFound();

            context.WorkExperiences.Remove(workExperience);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}