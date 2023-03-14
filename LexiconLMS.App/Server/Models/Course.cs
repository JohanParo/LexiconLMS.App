using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Server.Models
{
#nullable disable
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Module> Modules { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Document>? Documents { get; set; }
    }
}
