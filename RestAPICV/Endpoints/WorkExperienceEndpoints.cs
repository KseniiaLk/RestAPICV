using RestAPICV.DTOs;
using RestAPICV.Services;

namespace RestAPICV.EndPoints
{
    public static class WorkExperienceEndpoints
    {
        public static void RegisterWorkExperienceEndpoints(this WebApplication app)
        {
            app.MapGet("/workexperience", async (WorkExperienceService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            });

            app.MapGet("/workexperience/{id}", async (int id, WorkExperienceService service) =>
            {
                var result = await service.GetByIdAsync(id);
                if (result == null)
                    return Results.NotFound();
                return Results.Ok(result);
            });

            app.MapPost("/workexperience", async (CreateWorkExperienceDTO dto, WorkExperienceService service) =>
            {
                var result = await service.CreateAsync(dto);
                return Results.Created($"/workexperience/{result.Id}", result);
            });

            app.MapPut("/workexperience/{id}", async (int id, UpdateWorkExperienceDTO dto, WorkExperienceService service) =>
            {
                var result = await service.UpdateAsync(id, dto);
                if (result == null)
                    return Results.NotFound();
                return Results.Ok(result);
            });

            app.MapDelete("/workexperience/{id}", async (int id, WorkExperienceService service) =>
            {
                var result = await service.DeleteAsync(id);
                if (!result)
                    return Results.NotFound();
                return Results.NoContent();
            });
        }
    }
}