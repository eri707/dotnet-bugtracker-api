using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    public class AddProjectViewModel // this model is for user input when adding 
    {
        [Required(AllowEmptyStrings = false), MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
