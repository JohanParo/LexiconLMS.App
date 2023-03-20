﻿using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.App.Shared
{
    public class ModuleDto : IEntityDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int CourseId { get; set; }
        public List<ActivityDto>? Activities { get; set; }
        public bool Published { get; set; } = true;

    }
}