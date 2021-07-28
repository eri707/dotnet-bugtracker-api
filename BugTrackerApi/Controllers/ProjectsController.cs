using BugTrackerApi.Models;
using BugTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Controllers
{
    [ApiController]
    [Route("api/projects")] // /projects
    public class ProjectsController : ControllerBase
    {   
        private IProjectsRepository _projectsRepository; 
        public ProjectsController(IProjectsRepository projectsRepository) // injects an instance from DI container(see:ConfigureServices)
        {
            _projectsRepository = projectsRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Project>> GetAllProjects() // IEnumerable<Project> type is more usefule in terms of manipulating data
        {
            return _projectsRepository.GetAllProjects();
        }

        [HttpGet("{id}")]
        public async Task<Project> GetProject(Guid id) 
        {
            return _projectsRepository.GetProject(id);
        }

        [HttpPut("{id}")] // Update with UpddateProjectViewModel
        public async Task<Project> UpdateProject(Guid id, UpdateProjectViewModel model)
        {
            return _projectsRepository.UpdateProject(id, model);
        }

        [HttpPost] // Add with AddProjectViewModel
        public async Task<Project> AddProject(AddProjectViewModel model)
        {
            return _projectsRepository.AddProject(model);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProject(Guid id) 
        {
            _projectsRepository.DeleteProject(id);
            return Ok(); // return to the user(browser)
        }
    }
}
