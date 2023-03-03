using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexiconLMS.Shared.Entities;

namespace LexiconLMS.App.Shared.Entities
{
	internal class DocumentType
	{
		public int Id { get; set; }
		public string Type { get; set; }

		public ICollection<Document> Documents{ get; set; }
	}
}
