using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconLMS.App.Server.Models
{
#nullable disable
    public class DocumentType
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;

        public ICollection<Document> Documents { get; set; }
    }
}
