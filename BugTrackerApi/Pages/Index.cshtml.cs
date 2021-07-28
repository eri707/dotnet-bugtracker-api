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

        public void OnGet() // this method will be invoked when the view page is opend
        {
            AllProjects = _projectsRepository.GetAllProjects();
        }
        
        public void OnPostDelete(Guid id) 
        {
            _projectsRepository.DeleteProject(id);
            // you need to get new all projects after deleting
            AllProjects = _projectsRepository.GetAllProjects();
        }
    }
}
