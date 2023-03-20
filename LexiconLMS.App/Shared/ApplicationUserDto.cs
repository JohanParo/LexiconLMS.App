using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Shared
{
    public class ApplicationUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public string Avatar { get; set; }
        public bool Published { get; set; } = true;

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
 ErrorMessage = "Not a valid Email")]
        public string Email { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
    }
}
