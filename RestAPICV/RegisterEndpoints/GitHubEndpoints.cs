using RestAPICV.DTOs;
using System.Text.Json;

namespace RestAPICV.Endpoints
{
    public static class GitHubEndpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/github")
                .WithTags("GitHub")
                .WithOpenApi();

            group.MapGet("/repositories/{username}", GetGitHubRepositories);
        }

        private static async Task<IResult> GetGitHubRepositories(string username, HttpClient httpClient)
        {
            try
            {
                var response = await httpClient.GetAsync($"https://api.github.com/users/{username}/repos");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var repositories = JsonSerializer.Deserialize<List<GitHubRepo>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (repositories == null)
                        return Results.Ok(new List<GitHubRepositoryDTO>());

                    var result = repositories.Select(repo => new GitHubRepositoryDTO
                    {
                        Name = repo.Name ?? "Saknas",
                        Language = string.IsNullOrEmpty(repo.Language) ? "okänt" : repo.Language,
                        Description = string.IsNullOrEmpty(repo.Description) ? "saknas" : repo.Description,
                        Url = repo.HtmlUrl ?? "Saknas"
                    }).ToList();

                    return Results.Ok(result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Results.NotFound($"GitHub user '{username}' not found");
                }
                else
                {
                    return Results.Problem($"GitHub API returned status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error calling GitHub API: {ex.Message}");
            }
        }
    }

    public class GitHubRepo
    {
        public string? Name { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }
        public string? HtmlUrl { get; set; }
    }
}