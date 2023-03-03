using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.Shared.Entities
{
	internal class ActivityType
	{
		public int Id { get; set; }
		public string Type { get; set; }

		public ICollection<Activity> Activities { get; set; }
	}
}
