using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BugTrackerApi
{
    public class Program // Set up a host(The host is typically configured, built, and run by code in the Main method)        
    {
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            IHost built = hostBuilder.Build(); // Run the given actions to initialize the host. This can only be called once.
            built.Run(); // Runs an application and block the calling thread until host shutdown.
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args)
        {  // Host is a static class which doesn't need to create instanciation(static class is always running)
            IHostBuilder builder = Host.CreateDefaultBuilder(args); // returns IHostBuilder
            builder.ConfigureWebHostDefaults(w => // Expression Lambda
            { // data type is IWebHostBuilder
              // the value is resigned to IHostBiulder builder
                w.UseStartup<Startup>(); //Specify the startup type to be used by the web host.
            });
            return builder; // data type is IHostBuilder

        }

    }
}
