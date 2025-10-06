using System.ComponentModel.DataAnnotations;

namespace RestAPICV.DTOs
{
    public class EducationDTO
    {
        public int Id { get; set; }
        public string SchoolName { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PersonalInfoId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateEducationDTO
    {
        [Required]
        [StringLength(200)]
        public string SchoolName { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Degree { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int PersonalInfoId { get; set; }
    }

    public class UpdateEducationDTO
    {
        [Required]
        [StringLength(200)]
        public string SchoolName { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Degree { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}