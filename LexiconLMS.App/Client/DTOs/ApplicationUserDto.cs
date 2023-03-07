using Microsoft.AspNetCore.Identity;

namespace LexiconLMS.App.Client.DTOs
{
    public class ApplicationUserDto : IdentityUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public string Avatar { get; set; }

        //public  Course? Course { get; set; }
    }
}
