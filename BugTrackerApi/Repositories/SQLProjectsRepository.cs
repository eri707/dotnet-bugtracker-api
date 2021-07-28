using BugTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper; // mapping tool between C# ojecets and database
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BugTrackerApi.Repositories
{
    public class SQLProjectsRepository : IProjectsRepository // from DI(startup.cs)
    {
        private IConfiguration _config;
        private string _connString;

        public SQLProjectsRepository(IConfiguration configuration) // injects an instance from CreateDefaultBuilder(Program.cs) to access the appsetting.json??
        {
            _config = configuration;
            _connString = _config.GetConnectionString("Local"); // from appsettings.json
        }
        public  Project AddProject(AddProjectViewModel model) // add a project to sqldatabase 
        {
            var id = Guid.NewGuid(); 
            using (var db = new SqlConnection(_connString)) // use using statement since accessing databese is unmanaged resource(avoid memory leak)
            {  // return type int(must be 1 row)
                var result = db.Execute("INSERT INTO Projects (Id, CreatedOn, Name, Description) VALUES (@Id, @CreatedOn, @Name, @Description)", new { Id = id, CreatedOn = DateTime.UtcNow, Name = model.Name, Description = model.Description }); // this is new anonymous object
                if (result > 0)
                {
                    return GetProject(id); // return browser to represent the project which has just added
                }
                return null;
            }
        }

        public void DeleteProject(Guid id)
        {
            using(var db = new SqlConnection(_connString))
            {
                var result = db.Execute("DELETE FROM Projects WHERE Id = @Id", new { Id = id });
            }
        }

        public IEnumerable<Project> GetAllProjects()
        {
            using (var db = new SqlConnection(_connString))
            {
                var projects = db.Query<Project>($"SELECT * FROM Projects");
                return projects;
            }
        }

        public Project GetProject(Guid id)
        {
            using (var db = new SqlConnection(_connString))
            {
                // get the data from database (return IEnumerable)
                // this returns a list of IEnumerable(prural) 
                var projects = db.Query<Project>($"SELECT * FROM Projects WHERE Id = '{ id }'");
                // get me just one project from the list of IEnumerable
                var project = projects.FirstOrDefault();
                // get me the project or null 
                return project;
            }
        }

        public Project UpdateProject(Guid id, UpdateProjectViewModel model)
        {
            using (var db = new SqlConnection(_connString))
            {
                var result = db.Execute(@$"
                    UPDATE Projects 
                    SET Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END, -- case statement with SQL 
                        Description = CASE WHEN @Description IS NULL THEN Description ELSE @Description END 
                    WHERE id = '{id}'", model);
                if (result > 0)
                {
                    return GetProject(id);
                }
                return null;
            }
        }
    }
}
