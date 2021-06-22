using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    public class Bug
    {   // this id is for Bug
        public Guid Id { get; set; }
        // this id is for project
        public Guid ProjectId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Title { get; set; }
        // set that this is nullable*(except string) in norder to use ?? statement
        public Priority? Priority { get; set; }
        public string Description { get; set; }
       // Reproduce steps for bugs
        public string ReproSteps { get; set; }
        public string ActualResults { get; set; }
        public string ExpectedResults { get; set; }
        

    }
}
