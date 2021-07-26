using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BugTrackerApi
{
    public class Program // configures the app infrastructure(Web host, logging, DI container, IIS integration etc)       
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); 
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args)
        {   // Host doesn't need to be instanciated since it's static class
            IHostBuilder builder = Host.CreateDefaultBuilder(args); 
            builder.ConfigureWebHostDefaults(w =>
            { 
                w.UseStartup<Startup>(); //Specify startup class to be used by the web host.
            });
            return builder; 
        }
    }
}
