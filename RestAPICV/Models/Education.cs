using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPICV.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SchoolName { get; set; } = null!;

        [Required]
        public string Degree { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }
        public virtual PersonalInfo PersonalInfo { get; set; } = null!;
    }
}