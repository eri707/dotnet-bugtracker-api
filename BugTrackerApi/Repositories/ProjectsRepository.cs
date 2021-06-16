using BugTrackerApi.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Repositories
{
    public interface IProjectsRepository // what this can do
    {
        // add a project using AddProjectViewModel(format)
        Project AddProject(AddProjectViewModel model);
        // get a IEnumerable(list) of projects
        IEnumerable<Project> GetAllProjects();
        // get a project
        Project GetProject(Guid id);
        Project UpdateProject(int id);
        void DeleteProject(Guid id);
    }
    public class ProjectsRepository : IProjectsRepository
    {
        private IMemoryCache _cache;

        public ProjectsRepository(IMemoryCache memory)
        {
            _cache = memory;
        }
        public Project AddProject(AddProjectViewModel model)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now
            };
            // write how it can add a project
            _cache.Set(project.Id, project);
            return project;
        }

        public Project GetProject(Guid id)
        {
            return _cache.Get<Project>(id);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            // write how it can get a project
            throw new NotImplementedException();
        }

        public Project UpdateProject(int id)
        {
            throw new NotImplementedException();
        } 
        public void DeleteProject(Guid id)
        {
             _cache.Remove(id);
        }

        
    }
}
