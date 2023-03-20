using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Shared
{
    public interface IEntityDto
    {
        int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        string Title { get; set; } 
        string Description { get; set; }
        [Required]
        DateTime StartTime { get; set; }
        [Required]
        DateTime EndTime { get; set; }
        bool Published { get; set; }
    }
}