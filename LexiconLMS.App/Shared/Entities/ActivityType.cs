using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.Shared.Entities
{
#nullable disable
	public class ActivityType
	{
		public int Id { get; set; }
		public string Type { get; set; } = string.Empty;

		public ICollection<Activity> Activities { get; set; }
	}
}
