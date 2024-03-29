﻿using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LexiconLMS.App.Server.Models
{
#nullable disable
    public class Module
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int CourseId { get; set; }
        public List<Activity> Activities { get; set; }
        public bool Published { get; set; } = true;
    }
}
