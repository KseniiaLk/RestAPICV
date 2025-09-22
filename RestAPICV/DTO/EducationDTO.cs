using System.ComponentModel.DataAnnotations;

namespace RestAPICV.DTOs
{
    public class EducationDTO
    {
        public int EducationId { get; set; }
        public string SchoolName { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateEducationDTO
    {
        [Required]
        [MaxLength(200)]
        public string SchoolName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Degree { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int PersonId { get; set; }
    }

    public class UpdateEducationDTO
    {
        [Required]
        [MaxLength(200)]
        public string SchoolName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Degree { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}