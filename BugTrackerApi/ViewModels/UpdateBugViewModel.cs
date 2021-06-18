﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    public class UpdateBugViewModel
    {
        [MaxLength(255)]
        public string Title { get; set; }
        public Enum Priority { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string ReproSteps { get; set; }
        [MaxLength(1000)]
        public string ActualResults { get; set; }
        [MaxLength(1000)]
        public string ExpectedResults { get; set; }
        public Project Id { get; set; }

    }
}