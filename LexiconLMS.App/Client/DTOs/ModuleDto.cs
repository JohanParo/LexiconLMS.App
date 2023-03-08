using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Client.DTOs
{

    public class ModuleDto : IEntityDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseId { get; set; }
        public List<ActivityDto> Activities { get; set; }
    }
}
