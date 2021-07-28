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
    [Route("api/bugs")] // /bugs for endpoint routing

    public class BugsController : ControllerBase
    { 
        private IBugsRepository _bugsRepository; 
        private IProjectsRepository _projectRepository;

        public BugsController(IBugsRepository bugsRepository, IProjectsRepository projectsRepository)
        {
            _bugsRepository = bugsRepository;
            _projectRepository = projectsRepository;
        }
        [HttpGet("{id}")]
        public async Task<Bug> GetBug(Guid id)
        {
            return _bugsRepository.GetBug(id);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IEnumerable<Bug>> GetAllBugs(Guid projectId) 
        {   // get all bugs from one project with porojectId
            return _bugsRepository.GetAllBugs(projectId);
        }

        [HttpPut("{id}")]
        public async Task<Bug> UpdateBug(Guid id, UpdateBugViewModel model) // Update with UpddateProjectViewModel
        {
            return _bugsRepository.UpdateBug(id, model);
        }

        [HttpPost]
        public async Task<ActionResult<Bug>> AddBug(AddBugViewModel model) // Add with AddProjectViewModel
        {   // This is an error handling which can make sure if the Projectid exist when user put projectId.
            var project = _projectRepository.GetProject(model.ProjectId); // specify the project with the projectId put by user
            if (project == null) return new BadRequestObjectResult("ProjectId doesn't exist."); // if the project exists? then add bug. No? then return resutl.
            return _bugsRepository.AddBug(model);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBug(Guid id)
        {
            _bugsRepository.DeleteBug(id);
            return Ok();
        }
    }
}
