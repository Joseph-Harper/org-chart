using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OrgChart.Web.Extensions;

namespace OrgChart.Web
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).SeedData().Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
