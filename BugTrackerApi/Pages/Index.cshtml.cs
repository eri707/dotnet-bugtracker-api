using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApi.Models;
using BugTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApi.Pages // Get data from projectRepository through browser
{
    public class IndexModel : PageModel //is the page model for the Razor view
    {
        private IProjectsRepository _projectsRepository;
        // this field must be public in ordet to use it in Index.cshtml
        public IEnumerable<Project> AllProjects; // public variable convention capital case
      
        public IndexModel(IProjectsRepository projectsRepository) 
        {
            _projectsRepository = projectsRepository;
        }

        public void OnGet() // no return 
        {
            AllProjects = _projectsRepository.GetAllProjects();
        }
        
        public void OnPostDelete(Guid id) // no return 
        {
            _projectsRepository.DeleteProject(id);
            // what does this mean??
            AllProjects = _projectsRepository.GetAllProjects();
        }
    }
}
