using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Shared
{
    public class CourseDto : IEntityDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public List<ModuleDto>? Modules { get; set; }
        public List<ApplicationUserDto>? Users { get; set; }
        public bool Published { get; set; } = true;
    }
}
