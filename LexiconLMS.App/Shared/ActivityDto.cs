using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Shared
{
    public class ActivityDto : IEntityDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(typeof(DateTime), "1/2/2020", "1/1/2050",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Required]
        [Range(typeof(DateTime), "1/2/2020", "1/1/2050",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime EndTime { get; set; } = DateTime.Now;

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }
        public string ActivityTypeType { get; set; } = string.Empty;
        public bool Published { get; set; } = true;
    }

}
