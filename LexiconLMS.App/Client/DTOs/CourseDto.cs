using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Client.DTOs
{
#nullable disable
    public class CourseDto:IEntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<ModuleDto> Modules { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
    }
}
