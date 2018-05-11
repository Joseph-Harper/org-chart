using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OrgChart.Data;
using OrgChart.Infrastructure.Identity;

namespace OrgChart.Web.Extensions
{
    public static class WebHostExstensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                var user = userManager.FindByNameAsync("user@test.com").Result;
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Email = "user@test.com",
                        UserName = "user@test.com",
                        Id = "00000000-0000-0000-0000-000000000000"
                    };
                    var result = userManager.CreateAsync(user, "Admin1!").Result;
                }
                var context = scope.ServiceProvider.GetService<OrgChartDbContext>();
                OrgChartDbContextInitializer.SeedAsync(context, user.Id).Wait();
            }

            return host;
        }
    }
}
