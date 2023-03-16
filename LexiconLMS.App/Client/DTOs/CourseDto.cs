using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LexiconLMS.App.Client.DTOs
{
#nullable disable
    public class CourseDto:IEntityDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Start Time")]
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public List<ModuleDto> Modules { get; set; }
        public List<ApplicationUserDto> Users { get; set; }

        public List<string> GetCourseMembers()
        {
            return this.GetType().GetProperties().Select(pi => pi.GetCustomAttribute<DisplayAttribute>()?.Name?? pi.Name).ToList();
        }
    }
}
