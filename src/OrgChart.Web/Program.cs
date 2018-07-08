using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OrgChart.Web.Extensions;

namespace OrgChart.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            var hostingEnvironment = (IHostingEnvironment)webHost.Services.GetService(typeof(IHostingEnvironment));
            if (hostingEnvironment.IsDevelopment())
            {
                webHost = webHost.SeedData();
            }
            webHost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
