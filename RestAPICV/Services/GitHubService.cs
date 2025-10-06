using RestAPICV.DTOs;
using System.Text.Json;

namespace RestAPICV.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GitHubRepositoryDTO>> GetRepositoriesAsync(string username)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/repos");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var repositories = JsonSerializer.Deserialize<List<GitHubRepo>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (repositories == null)
                        return new List<GitHubRepositoryDTO>();

                    return repositories.Select(repo => new GitHubRepositoryDTO
                    {
                        Name = repo.Name ?? "Saknas",
                        Language = string.IsNullOrEmpty(repo.Language) ? "okänt" : repo.Language,
                        Description = string.IsNullOrEmpty(repo.Description) ? "saknas" : repo.Description,
                        Url = repo.HtmlUrl ?? "Saknas"
                    }).ToList();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception($"GitHub user '{username}' not found");
                }
                else
                {
                    throw new Exception($"GitHub API returned status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling GitHub API: {ex.Message}");
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