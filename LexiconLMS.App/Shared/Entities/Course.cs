using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.Shared.Entities
{
	internal class Course
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Descritpion { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public List<Module> Modules { get; set; }
		public List<User> Users { get; set; }
	}
}
