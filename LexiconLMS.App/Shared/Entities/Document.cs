using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexiconLMS.App.Shared.Entities;

namespace LexiconLMS.Shared.Entities
{
	internal class Document
	{
		public int Id { get; set; }
		public int CMAID { get; set; }
		public string Title { get; set; }
		public DocumentType DocumentType{ get; set; }
		public string Description { get; set; }
		public DateTime ShowTime { get; set; }
		public bool IsHidden { get; set; }
		public string DocumentFile{ get; set; }
		public int UserId { get; set; }
	}
}
