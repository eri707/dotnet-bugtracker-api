﻿using BugTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BugTrackerApi.Repositories
{
    public class SQLBugsRepository : IBugsRepository // from DI(startup.cs)
    {
        private IConfiguration _config;
        private string _connString;

        public SQLBugsRepository(IConfiguration configuration)
        {
            _config = configuration;
            _connString = configuration.GetConnectionString("Local");
        }
        public Bug AddBug(AddBugViewModel model)
        {
            var id = Guid.NewGuid();
            using (var db = new SqlConnection(_connString))
            { // db.Execute("SQL code", anonymous object (to be replaced into values))
                var result = db.Execute("INSERT INTO Bugs(Id, CreatedOn,ProjectId, Title, Priority, Description, ReproSteps, ActualResults, ExpectedResults) VALUES(@Id, @CreatedOn, @ProjectId, @Title, @Priority, @Description, @ReproSteps, @ActualResults, @ExpectedResults)", new { Id = id, CreatedOn = DateTime.UtcNow, ProjectId = model.ProjectId, Title = model.Title, Priority = model.Priority, Description = model.Description, ReproSteps = model.ReproSteps, ActualResults = model.ActualResults, ExpectedResults = model.ExpectedResults });
                if (result > 0)
                    return GetBug(id);
            }
            return null;
        }

        public void DeleteBug(Guid id)
        {
            using (var db = new SqlConnection(_connString))
            {
                var result = db.Execute("DELETE FROM Bugs WHERE Id = @Id", new { Id = id });
            }
                
        }

        public IEnumerable<Bug> GetAllBugs(Guid projectId)
        {
            using (var db = new SqlConnection(_connString))
            {
                var bugs = db.Query<Bug>($"SELECT * FROM Bugs WHERE ProjectId = '{ projectId }'");
                return bugs;
            }
        }

        public Bug GetBug(Guid id)
        {
            using (var db = new SqlConnection(_connString))
            {
                var bugs = db.Query<Bug>($"SELECT * FROM Bugs WHERE Id = '{ id }'");
                return bugs.FirstOrDefault();
            }
        }

        public Bug UpdateBug(Guid id, UpdateBugViewModel model)
        {
            using (var db = new SqlConnection(_connString))
            {
                var result = db.Execute($"UPDATE Bugs SET Title = @Title, Priority = @Priority, Description = @Description, ReproSteps = @ReproSteps, ActualResult = @ActualResult, ExpectedResults = @ExpectedResults WHERE Id = '{ id }'", model); // model??
                if (result > 0)
                {
                    return GetBug(id);
                }
                return null;
            }
        }
    }
}
