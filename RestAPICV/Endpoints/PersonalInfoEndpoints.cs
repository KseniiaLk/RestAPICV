using RestAPICV.DTOs;
using RestAPICV.Services;

namespace RestAPICV.EndPoints
{
    public static class PersonalInfoEndpoints
    {
        public static void RegisterPersonalInfoEndpoints(this WebApplication app)
        {
            app.MapGet("/personalinfo", async (PersonalInfoService service) =>
            {
                var result = await service.GetAllAsync();
                return Results.Ok(result);
            });

            app.MapGet("/personalinfo/{id}", async (int id, PersonalInfoService service) =>
            {
                var result = await service.GetByIdAsync(id);
                if (result == null)
                    return Results.NotFound();
                return Results.Ok(result);
            });

            app.MapPost("/personalinfo", async (CreatePersonalInfoDTO dto, PersonalInfoService service) =>
            {
                var result = await service.CreateAsync(dto);
                return Results.Created($"/personalinfo/{result.Id}", result);
            });

            app.MapPut("/personalinfo/{id}", async (int id, UpdatePersonalInfoDTO dto, PersonalInfoService service) =>
            {
                var result = await service.UpdateAsync(id, dto);
                if (result == null)
                    return Results.NotFound();
                return Results.Ok(result);
            });

            app.MapDelete("/personalinfo/{id}", async (int id, PersonalInfoService service) =>
            {
                var result = await service.DeleteAsync(id);
                if (!result)
                    return Results.NotFound();
                return Results.NoContent();
            });
        }
    }
}