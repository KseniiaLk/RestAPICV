using System.ComponentModel.DataAnnotations;

namespace RestAPICV.DTOs
{
    public class WorkExperienceDTO
    {
        public int WorkExperienceId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Year { get; set; }
        public int PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateWorkExperienceDTO
    {
        [Required]
        [MaxLength(200)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Company { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        public int PersonId { get; set; }
    }

    public class UpdateWorkExperienceDTO
    {
        [Required]
        [MaxLength(200)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Company { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }
    }
}