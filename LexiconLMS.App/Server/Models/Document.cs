using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Server.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        //public int? CMAID { get; set; }
        public string Title { get; set; }
        //public DocumentType DocumentType { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public bool IsHidden { get; set; } = true;
        public int ErrorCode { get; set; }
        public string? FileType { get; set; }
        public bool Uploaded { get; set; }
        //public string DocumentFile { get; set; }
        //[Required]
        //public int UserId { get; set; }

        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
    }
}
