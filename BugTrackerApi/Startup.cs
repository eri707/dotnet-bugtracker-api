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
    public class Startup // The Startup class configures services and the app's request pipeline
    {   // property has a hidden field so you can use property name as the filed
        public IConfiguration Configuration { get; }
        // Only the following service types can be injected into the Startup constructor when using the Generic Host (IHostBuilder): IWebHostEnvironment IHostEnvironment IConfiguration
        public Startup(IConfiguration configuration) // injects an instance from CreateDefaultBuilder(Program.cs)
        {
            Configuration = configuration;
        }

        // means to add services into dependency injection container via ConfigureServices
        // all classes in this container are instanciated then can be utilized anywhere
        public void ConfigureServices(IServiceCollection services) // IserviceCollection is DI container
        {
            services.AddControllers(); // look for API controller and instanciates all of classes
            services.AddSwaggerGen(c =>
            { // This is the info on the swagger
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugTrackerApi", Version = "v1" });
            });
            
            // for IMemory chache
            // services.AddScoped<IProjectsRepository, ProjectsRepository>(); 
            // services.AddScoped<IBugsRepository, BugsRepository>(); 
            // for SQLdatabese
            services.AddScoped<IProjectsRepository, SQLProjectsRepository>(); 
            services.AddScoped<IBugsRepository, SQLBugsRepository>();
            
            // a local in-memory cache(temporary) whose values are not serialized and needs 2 parameters(key, value) using Cache like Dictionary
            services.AddMemoryCache();
            // for using Razor
            services.AddRazorPages();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Creates middlewares in order
        { 
            if (env.IsDevelopment()) // if the app running in the development mode(in environmentVariables)
            {  // enable to use swagger's suport
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BugTrackerApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {   // connects all controllers to route
                endpoints.MapControllers();
                // connects RazorPages to route
                endpoints.MapRazorPages();
            });
        }
    }
}
