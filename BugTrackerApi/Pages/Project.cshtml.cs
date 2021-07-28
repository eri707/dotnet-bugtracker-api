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
    public class ProjectModel : PageModel
    {
        private IProjectsRepository _projectsRepository;
        private IBugsRepository _bugsRepository;
        public Project Project;
        public IEnumerable<Bug> Bugs;

        public ProjectModel(IProjectsRepository projectsRepository, IBugsRepository bugsRepository)
        {
            _projectsRepository = projectsRepository;
            _bugsRepository = bugsRepository;
        }
        public void OnGet(Guid id) // get one project
        {
            Project = _projectsRepository.GetProject(id); 
            Bugs = _bugsRepository.GetAllBugs(id);
        }
        public void OnPostDelete(Guid id)
        {
            var bug = _bugsRepository.GetBug(id);
            _bugsRepository.DeleteBug(id);
            // you need to get new all bugs after deleting
            Bugs = _bugsRepository.GetAllBugs(bug.ProjectId);
        }

    }
}
