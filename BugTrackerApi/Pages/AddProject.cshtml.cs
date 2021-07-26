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
        [BindProperty] // the process that takes values from HTTP requests and maps them to handler method parameters or PageModel properties
        public AddProjectViewModel Project { get; set; } // ??

        public AddProjectModel(IProjectsRepository projectsRepository) 
        {
            _projectsRepository = projectsRepository;
        }
        
        public ActionResult OnPost() // where should it be used??
        { // what is ModelState??
            if (!ModelState.IsValid)
            {
                return Page(); //??
            }
            _projectsRepository.AddProject(Project); // what is this Project??
            return RedirectToPage("/Index"); // why Index??
        }
    }
}
