namespace RestAPICV.DTOs
{
    public class GitHubRepositoryDTO
    {
        public string Name { get; set; } = null!;
        public string Language { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}