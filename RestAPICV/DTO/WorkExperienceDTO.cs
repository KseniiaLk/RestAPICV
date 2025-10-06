using System.ComponentModel.DataAnnotations;

namespace RestAPICV.DTOs
{
    public class WorkExperienceDTO
    {
        public int Id { get; set; }
        public string JobTitle { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string? Description { get; set; }
        public int Year { get; set; }
        public int PersonalInfoId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateWorkExperienceDTO
    {
        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Company { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        public int PersonalInfoId { get; set; }
    }

    public class UpdateWorkExperienceDTO
    {
        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Company { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }
    }
}