using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LexiconLMS.App.Server.Models
{
#nullable disable
    public class ApplicationUser : IdentityUser
    {
        //public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public string Avatar { get; set; }
        public bool Published { get; set; } = true;

        //public  Course? Course { get; set; }
    }
}
