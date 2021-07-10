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
    {   // This block means to be able to use IProjectsRepository in this class Controller to use data from _projectsRepository
        private IProjectsRepository _projectsRepository; // _ means private variable
        public ProjectsController(IProjectsRepository projectsRepository) // injects an instance from DI container(see:ConfigureServices)
        {
            _projectsRepository = projectsRepository;
        }
        // action method
        [HttpGet]
        // IEnumerable<Project> type is more usefule in terms of manipulating data
        public async Task<IEnumerable<Project>> GetAllProjects() 
        {
            return _projectsRepository.GetAllProjects();
        }
        [HttpGet("{id}")]
        public async Task<Project> GetProject(Guid id) 
        {
            return _projectsRepository.GetProject(id);
        }
        [HttpPut("{id}")]
        public async Task<Project> UpdateProject(Guid id, UpdateProjectViewModel model)
        {
            return _projectsRepository.UpdateProject(id, model);
        }
        [HttpPost]
        public async Task<Project> AddProject(AddProjectViewModel model)
        {
            return _projectsRepository.AddProject(model);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            _projectsRepository.DeleteProject(id);
            return Ok();
        }
        
    }
}
