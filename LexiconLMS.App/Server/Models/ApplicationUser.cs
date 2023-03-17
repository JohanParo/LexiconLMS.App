using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LexiconLMS.App.Server.Models
{
#nullable disable
    public class ApplicationUser : IdentityUser
    {
        //public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public string Avatar { get; set; }
        public bool Published { get; set; } = true;

        //public  Course? Course { get; set; }
    }
}
