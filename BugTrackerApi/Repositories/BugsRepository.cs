using BugTrackerApi.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Repositories
{
    public interface IBugsRepository
    {
        Bug AddBug(AddBugViewModel model); // adding with AddBugViewModel
        Bug GetBug(Guid id);
        IEnumerable<Bug> GetAllBugs(Guid projectId);
        Bug UpdateBug(Guid id, UpdateBugViewModel model); // updating with UpdateBugViewModel
        void DeleteBug(Guid id);
    }
    public class BugsRepository : IBugsRepository
    {
        private IMemoryCache _cache;
        public BugsRepository(IMemoryCache memory)
        {
            _cache = memory;
        }
        public Bug AddBug(AddBugViewModel model)
        {
            var bug = new Bug
            {
                Title = model.Title,
                ProjectId = model.ProjectId,
                Priority = model.Priority,
                Description = model.Description,
                ReproSteps = model.ReproSteps,
                ActualResults = model.ActualResults,
                ExpectedResults = model.ExpectedResults,
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now
            };
            _cache.Set($"bug_{bug.Id}", bug);
            return bug;
        }
        public void DeleteBug(Guid id)
        {
            _cache.Remove($"bug_{id}");
        }
        public IEnumerable<Bug> GetAllBugs(Guid projectId)
        {   
            var keys = _cache.GetKeys<string>(); //get all "project_{id}" and "bug_{id}"
            var bugs = new List<Bug>();
            foreach (var key in keys)
            {   
                if (!key.StartsWith("bug_")) continue; // if not start with "bug_", then skip
                bugs.Add(_cache.Get<Bug>(key));
            } // select the bug which is correspond with projectId put by user
            return bugs.Where(p => p.ProjectId == projectId); //lambda expression to query data from database
        }
        public Bug GetBug(Guid id)
        {
            return _cache.Get<Bug>($"bug_{id}");
        }
        public Bug UpdateBug(Guid id, UpdateBugViewModel model)
        {   
            var updateBug = _cache.Get<Bug>($"bug_{id}");
            updateBug.Title = model.Title ?? updateBug.Title;
            updateBug.Priority = model.Priority ?? updateBug.Priority;
            updateBug.Description = model.Description ?? updateBug.Description;
            updateBug.ReproSteps = model.ReproSteps ?? updateBug.ReproSteps;
            updateBug.ActualResults = model.ActualResults ?? updateBug.ActualResults;
            updateBug.ExpectedResults = model.Title ?? updateBug.ExpectedResults;
            _cache.Set($"bug_{id}", updateBug);
            return updateBug;
        }
    }
}
