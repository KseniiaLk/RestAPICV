using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPICV.Models
{
    public class WorkExperience
    {
        [Key]
        public int WorkExperienceId { get; set; }

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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Needed foreign key
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual PersonalInfo Person { get; set; } = null!;
    }
}