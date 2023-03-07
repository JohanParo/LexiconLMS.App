using LexiconLMS.Shared.Entities;

namespace LexiconLMS.App.Client.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public List<Module> Modules { get; set; }
        //public List<ApplicationUser> Users { get; set; }
    }
}
