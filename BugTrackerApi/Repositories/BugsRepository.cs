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
        Bug AddBug(AddBugViewModel model);
        Bug GetBug(Guid id);
        IEnumerable<Bug> GetAllBugs();
        Bug UpdateBug(Guid id, UpdateBugViewModel model);
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
                Priority = model.Priority,
                Description = model.Description,
                ReproSteps = model.ReproSteps,
                ActualResults = model.ActualResults,
                ExpectedResults = model.ExpectedResults,
                Id = model.Id //??
            };
            _cache.Set(bug.Id, bug);
            return bug;
        }

        public void DeleteBug(Guid id)
        {
            _cache.Remove(id);
        }

        public IEnumerable<Bug> GetAllBugs()
        {
            var keys = _cache.GetKeys<Project>();
            var bugs = new List<Bug>();
            foreach (var key in keys)
            {
                bugs.Add(_cache.Get<Bug>(key));
            }
            return bugs;
        }
        public Bug GetBug(Guid id)
        {
            return _cache.Get<Bug>(id);
        }

        public Bug UpdateBug(Guid id, UpdateBugViewModel model)
        {
            var updateBug = _cache.Get<Bug>(id);
            updateBug.Title = model.Title ?? updateBug.Title;
            updateBug.Priority = model.Priority ?? updateBug.Priority;
            updateBug.Description = model.Description ?? updateBug.Description;
            updateBug.ReproSteps = model.ReproSteps ?? updateBug.ReproSteps;
            updateBug.ActualResults = model.ActualResults ?? updateBug.ActualResults;
            updateBug.ExpectedResults = model.Title ?? updateBug.ExpectedResults;
            updateBug.Id = model.Id ?? updateBug.Id;
            _cache.Set(id, updateBug);
            return updateBug;
        }
    }
}
