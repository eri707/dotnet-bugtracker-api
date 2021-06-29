using BugTrackerApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApi
{
    public class Startup
    {   // This block means to be able to use IConfiguration in class Startup from from CreateDefaultBuilder(Program.cs)
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) // injects an instance from CreateDefaultBuilder(Program.cs)
        {
            Configuration = configuration;
        }

        // means to add services into dependency injection container via ConfigureServices
        // all classes in this container are instanciated then can be utilized anywhere
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // look for API controller and instanciates all of classes
            services.AddSwaggerGen(c =>
            { // This is the info on the swagger
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugTrackerApi", Version = "v1" });
            });
            // implement the dependency container 
            services.AddScoped<IProjectsRepository, ProjectsRepository>(); // for IMemory chache
            // services.AddScoped<IProjectsRepository, SQLProjectsRepository>(); // for SQLdatabese
            services.AddScoped<IBugsRepository, BugsRepository>(); // for IMemory chache
            // services.AddScoped<IBugsRepository, SQLBugsRepository>(); // for SQLdatabese
            //  a local in-memory cache(temporary) whose values are not serialized
            // need 2 parameters(key, value) using Cache like Dictionary
            services.AddMemoryCache();
        }

        // Creates middlewares in order
        // IApplicationBuilder provides the mechanisms to configure an app's request pipeline
        // IWebHostEnvironment provides information about the web hosting emvironment an app is running in 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        { 
            if (env.IsDevelopment()) // if the app running in the development mode(in environmentVariables)
            {  // enable to use swagger's suport
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BugTrackerApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            // conects all controllers to route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
