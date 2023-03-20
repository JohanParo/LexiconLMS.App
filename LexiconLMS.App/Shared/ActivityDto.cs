using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Shared
{
    public class ActivityDto : IEntityDto
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

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }
        public string ActivityTypeType { get; set; } = string.Empty;
        public bool Published { get; set; } = true;
    }

}
