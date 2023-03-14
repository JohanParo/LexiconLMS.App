using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Client.DTOs
{
    public class DocumentDto
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ShowTime { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public bool IsHidden { get; set; }
        public int ErrorCode { get; set; }
        public string? FileType { get; set; }
        public bool Uploaded { get; set; }
        [Required]
        public int UserId { get; set; }

        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
    }
}
