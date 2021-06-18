using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    public class Project
    {
        // unique id(only in this class)
        public Guid Id { get; set; }
        // when was the project created(only in this class)
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
