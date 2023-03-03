using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.Shared.Entities
{
	internal class Activity
	{
		public int Id { get; set; }
		public ActivityType ActivityType { get; set; }
		public string Description { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public int ModuleId { get; set; }
	}

}
