using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPICV.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string SchoolName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Degree { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Needed foreign key
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual PersonalInfo Person { get; set; } = null!;
    }
}