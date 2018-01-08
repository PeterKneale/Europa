using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;

namespace Europa.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices(services => services.AddAutofac())
                .Build();
    }
}
