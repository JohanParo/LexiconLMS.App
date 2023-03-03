using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LexiconLMS.Shared.Entities
{
	public class ApplicationUser : IdentityUser
	{
		//public int Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int? CourseId { get; set; }
	}
}
