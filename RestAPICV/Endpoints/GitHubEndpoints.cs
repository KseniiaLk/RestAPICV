using RestAPICV.DTOs;
using RestAPICV.Services;

namespace RestAPICV.EndPoints
{
    public static class GitHubEndpoints
    {
        public static void RegisterGitHubEndpoints(this WebApplication app)
        {
            app.MapGet("/github/repositories/{username}", async (string username, GitHubService service) =>
            {
                try
                {
                    var repositories = await service.GetRepositoriesAsync(username);
                    return Results.Ok(repositories);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}