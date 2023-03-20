using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LexiconLMS.App.Server.Models
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
