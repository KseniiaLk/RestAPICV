using Microsoft.EntityFrameworkCore;
using RestAPICV.Data;
using RestAPICV.DTOs;
using RestAPICV.Models;

namespace RestAPICV.Endpoints
{
    public static class EducationEndpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/education")
                .WithTags("Education")
                .WithOpenApi();

            group.MapGet("/", GetAllEducation);
            group.MapGet("/{id}", GetEducationById);
            group.MapPost("/", CreateEducation);
            group.MapPut("/{id}", UpdateEducation);
            group.MapDelete("/{id}", DeleteEducation);
        }

        private static async Task<IResult> GetAllEducation(RestAPICVContext context)
        {
            var educations = await context.Educations.ToListAsync();
            return Results.Ok(educations.Select(e => new EducationDTO
            {
                EducationId = e.EducationId,
                SchoolName = e.SchoolName,
                Degree = e.Degree,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                PersonId = e.PersonId,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }));
        }

        private static async Task<IResult> GetEducationById(int id, RestAPICVContext context)
        {
            var education = await context.Educations.FindAsync(id);
            if (education == null)
                return Results.NotFound();

            var result = new EducationDTO
            {
                EducationId = education.EducationId,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                PersonId = education.PersonId,
                CreatedAt = education.CreatedAt,
                UpdatedAt = education.UpdatedAt
            };

            return Results.Ok(result);
        }

        private static async Task<IResult> CreateEducation(CreateEducationDTO dto, RestAPICVContext context)
        {
            var person = await context.PersonalInfos.FindAsync(dto.PersonId);
            if (person == null)
                return Results.BadRequest();

            var education = new Education
            {
                SchoolName = dto.SchoolName,
                Degree = dto.Degree,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PersonId = dto.PersonId,
                CreatedAt = DateTime.UtcNow
            };

            context.Educations.Add(education);
            await context.SaveChangesAsync();

            return Results.Created($"/api/education/{education.EducationId}", education);
        }

        private static async Task<IResult> UpdateEducation(int id, UpdateEducationDTO dto, RestAPICVContext context)
        {
            var education = await context.Educations.FindAsync(id);
            if (education == null)
                return Results.NotFound();

            education.SchoolName = dto.SchoolName;
            education.Degree = dto.Degree;
            education.StartDate = dto.StartDate;
            education.EndDate = dto.EndDate;
            education.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Results.Ok(education);
        }

        private static async Task<IResult> DeleteEducation(int id, RestAPICVContext context)
        {
            var education = await context.Educations.FindAsync(id);
            if (education == null)
                return Results.NotFound();

            context.Educations.Remove(education);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}