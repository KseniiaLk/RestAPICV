using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPICV.Models
{
    public class WorkExperience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; } = null!;

        [Required]
        public string Company { get; set; } = null!;

        public string? Description { get; set; }

        public int Year { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }
        public virtual PersonalInfo PersonalInfo { get; set; } = null!;
    }
}