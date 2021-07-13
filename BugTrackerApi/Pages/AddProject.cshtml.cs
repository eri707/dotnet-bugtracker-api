using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApi.Models;
using BugTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApi.Pages
{
    public class AddProjectModel : PageModel
    {
        private IProjectsRepository _projectsRepository;
        [BindProperty]
        public AddProjectViewModel Project { get; set; }

        public AddProjectModel(IProjectsRepository projectsRepository) // DI from startup
        {
            _projectsRepository = projectsRepository;
        }
        
        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _projectsRepository.AddProject(Project);
            return RedirectToPage("/Index");
        }
    }
}
