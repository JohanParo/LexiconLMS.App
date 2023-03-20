using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LexiconLMS.App.Shared
{
    public interface IEntityDto
    {
        int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters")]
        string Title { get; set; } 
        string Description { get; set; }
        [Required]
        DateTime StartTime { get; set; }
        [Required]
        DateTime EndTime { get; set; }
        bool Published { get; set; }
    }
}