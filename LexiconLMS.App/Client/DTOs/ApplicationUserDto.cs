using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LexiconLMS.App.Client.DTOs
{
    public class ApplicationUserDto : IdentityUser
    {
        //public string Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public int? CourseId { get; set; }
        public string Avatar { get; set; }
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
         ErrorMessage = "Not a valid Email")]
        public string Email { get; set; }

        //public  Course? Course { get; set; }
    }
}
