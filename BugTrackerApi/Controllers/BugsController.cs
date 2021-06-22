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
    [Route("bugs")] // /bugs

    public class BugsController : ControllerBase
    {
        private IBugsRepository _bugsRepository;

        public BugsController(IBugsRepository bugsRepository)
        {
            _bugsRepository = bugsRepository;
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
        public async Task<Bug> UpdateBug(Guid id, UpdateBugViewModel model)
        {
            return _bugsRepository.UpdateBug(id, model);
        }
        [HttpPost]
        public async Task<Bug> AddBug(AddBugViewModel model)
        {
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
