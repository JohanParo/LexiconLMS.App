using LexiconLMS.App.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Server
{
#nullable disable
	public class Activity
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

        public ActivityType ActivityType { get; set; }
        public bool Published { get; set; } = true;
    }

}
