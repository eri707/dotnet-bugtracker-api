using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    // this model from customers are send to API (Guid and DateTime are not necessary)
    public class UpdateProjectViewModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
