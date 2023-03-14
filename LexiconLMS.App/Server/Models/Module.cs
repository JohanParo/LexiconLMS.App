﻿using LexiconLMS.App.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Server
{
#nullable disable
	public class Module
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int CourseId { get; set; }
		public List<Activity> Activities { get; set; }
		public List<Document>? Documents { get; set; }
	}
}
