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
    public class EditProjectModel : PageModel
    {
        private IProjectsRepository _projectsRepository;
        [BindProperty] // the process that takes values from HTTP requests and maps them to handler method parameters or PageModel properties.
        public UpdateProjectViewModel Project { get; set; }
        public Guid ProjectId;

        public EditProjectModel(IProjectsRepository projectsRepository) 
        {
            _projectsRepository = projectsRepository;
        }
        public void OnGet(Guid id) 
        {
            ProjectId = id;
            var existingProject = _projectsRepository.GetProject(id);
            Project = new UpdateProjectViewModel();
            Project.Name = existingProject.Name;
            Project.Description = existingProject.Description;
        }
        
        public ActionResult OnPost(Guid id) // add the editted projoct
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _projectsRepository.UpdateProject(id, Project);
            return RedirectToPage("/Index");
        }
    }
}
