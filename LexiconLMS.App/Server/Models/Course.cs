using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LexiconLMS.App.Server.Models
{
#nullable disable
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public List<Module> Modules { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public bool Published { get; set; } = true;
    }
}
