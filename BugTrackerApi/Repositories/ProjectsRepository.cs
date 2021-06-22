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
        Project UpdateProject(Guid id, UpdateProjectViewModel model);
        void DeleteProject(Guid id);
    }
    public class ProjectsRepository : IProjectsRepository
    {
        private IMemoryCache _cache; // then be able to use this _cache belowe in methods.

        public ProjectsRepository(IMemoryCache memory) // injects an instance from DI container(see:ConfigureServices)
        {
            _cache = memory;
        }
        public Project AddProject(AddProjectViewModel model) 
        {   // Create a format for storing data into cache
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now
            };
            _cache.Set($"project_{Guid.NewGuid()}", project); // set key and value for storing data
            return project;
        }

        public Project GetProject(Guid id) // write how it can get a project
        {
            return _cache.Get<Project>($"project_{id}");
        }

        public IEnumerable<Project> GetAllProjects() 
        {
            var keys = _cache.GetKeys<string>(); // get all Guid Id
            var projects = new List<Project>(); // create a empty list of Project
            foreach (var key in keys)
            {
                if (!key.StartsWith("project_")) continue;
                projects.Add(_cache.Get<Project>(key)); // add each project with the key into an empty list of Project
            }
            return projects; // return to customer
        }

        public Project UpdateProject(Guid id, UpdateProjectViewModel model) 
        {   // retrieve project model from cache
            var updateProject = _cache.Get<Project>($"project_{id}");
            // update the properties
            updateProject.Name = model.Name ?? updateProject.Name; // model.Name is null? then use updateProject.Name
            updateProject.Description = model.Description ?? updateProject.Description;
            // set the updated project in cache
            _cache.Set($"project_{id}", updateProject);
            return updateProject; // return to customer
        } 
        public void DeleteProject(Guid id) // why is not this project_id? 
        {
             _cache.Remove($"project_{id}"); // no return 
        }

        
    }
}
