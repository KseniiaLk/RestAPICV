using System.ComponentModel.DataAnnotations;

namespace RestAPICV.DTOs
{
    public class PersonalInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<EducationDTO>? Educations { get; set; }
        public List<WorkExperienceDTO>? WorkExperiences { get; set; }
    }

    public class CreatePersonalInfoDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }
    }

    public class UpdatePersonalInfoDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }
    }
}