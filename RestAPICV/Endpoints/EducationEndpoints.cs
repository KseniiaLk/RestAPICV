using RestAPICV.DTOs;
using RestAPICV.Services;

namespace RestAPICV.EndPoints
{
    public static class EducationEndpoints
    {
        public static void RegisterEducationEndpoints(this WebApplication app)
        {
            app.MapGet("/education", async (EducationService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            });

            app.MapGet("/education/{id}", async (int id, EducationService service) =>
            {
                var result = await service.GetByIdAsync(id);
                if (result == null)
                    return Results.NotFound();
                return Results.Ok(result);
            });

            app.MapPost("/education", async (CreateEducationDTO dto, EducationService service) =>
            {
                var result = await service.CreateAsync(dto);
                return Results.Created($"/education/{result.Id}", result);
            });

            app.MapPut("/education/{id}", async (int id, UpdateEducationDTO dto, EducationService service) =>
            {
                var result = await service.UpdateAsync(id, dto);
                if (result == null)
                    return Results.NotFound();
                return Results.Ok(result);
            });

            app.MapDelete("/education/{id}", async (int id, EducationService service) =>
            {
                var result = await service.DeleteAsync(id);
                if (!result)
                    return Results.NotFound();
                return Results.NoContent();
            });
        }
    }
}