using Microsoft.AspNetCore.Identity;

namespace LexiconLMS.App.Client.DTOs
{
    public class ApplicationUserDto : IdentityUser
    {
        //public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public int? CourseId { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }

        //public  Course? Course { get; set; }
    }
}
