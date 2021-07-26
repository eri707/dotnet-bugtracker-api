using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Models
{
    public class Bug // this model is for database when stored
    {   
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Title { get; set; }
        public Priority? Priority { get; set; }
        public string Description { get; set; }
        public string ReproSteps { get; set; }
        public string ActualResults { get; set; }
        public string ExpectedResults { get; set; }
        

    }
}
