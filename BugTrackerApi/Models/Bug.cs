using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    public class Bug
    {
        public string Title { get; set; }
        // Enum is short for "enumerations", which means "specifically listed"
        public Enum Priority { get; set; }
        public string Description { get; set; }
       // Reproduce steps for bugs
        public string ReproSteps { get; set; }
        public string ActualResults { get; set; }
        public string ExpectedResults { get; set; }
        public Project Id { get; set; }

    }
}
