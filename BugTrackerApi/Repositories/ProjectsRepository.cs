using BugTrackerApi.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi.Repositories
{
    public interface IProjectsRepository // what this must do
    {
        Project AddProject(AddProjectViewModel model); // adding with AddProjectViewModel
        IEnumerable<Project> GetAllProjects();
        Project GetProject(Guid id);
        Project UpdateProject(Guid id, UpdateProjectViewModel model); // updating with UpdateProjectViewModel
        void DeleteProject(Guid id);
    }
    public class ProjectsRepository : IProjectsRepository
    {
        private IMemoryCache _cache; 
        public ProjectsRepository(IMemoryCache memory) // injects an instance from DI container(see:ConfigureServices)
        {
            _cache = memory;
        }
        public Project AddProject(AddProjectViewModel model) 
        {   
            var id = Guid.NewGuid();
            var project = new Project // instanciate Project model for storing data into cache
            {
                Name = model.Name,
                Description = model.Description,
                Id = id,
                CreatedOn = DateTime.Now
            };
            _cache.Set($"project_{id}", project); // set key and value for storing data
            return project;
        }
        public Project GetProject(Guid id) 
        {
            return _cache.Get<Project>($"project_{id}");
        }
        public IEnumerable<Project> GetAllProjects() 
        {
            var keys = _cache.GetKeys<string>(); // get all "project_{id}" and "bug_{id}"
            var projects = new List<Project>(); // create a empty list of Project
            foreach (var key in keys)
            {
                if (!key.StartsWith("project_")) continue;
                projects.Add(_cache.Get<Project>(key)); // add each project with the key
            }
            return projects; 
        }
        public Project UpdateProject(Guid id, UpdateProjectViewModel model) 
        {   
            var updateProject = _cache.Get<Project>($"project_{id}");
            // update the values
            updateProject.Name = model.Name ?? updateProject.Name; // model.Name is null? then use updateProject.Name
            updateProject.Description = model.Description ?? updateProject.Description;
            _cache.Set($"project_{id}", updateProject);
            return updateProject; 
        } 
        public void DeleteProject(Guid id) // can i use ActionResult here??
        {
             _cache.Remove($"project_{id}"); 
        }
    }
}
