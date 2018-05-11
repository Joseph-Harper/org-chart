using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrgChart.Core.Interfaces;
using OrgChart.Data;
using OrgChart.Infrastructure.Identity;
using OrgChart.Web.Extensions;

namespace OrgChart.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseInMemoryDatabase("Identity"));
            services.AddDbContext<OrgChartDbContext>(options => options.UseInMemoryDatabase("OrgChart"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            services.AddScoped<IAuthorizationHandler, EntityAuthorizationHandler>();
            services.AddScoped<IUserProvider, IdentityUserProvider>();

            services.AddMvc(config =>
            {
                // Require authentication globally. Mark exceptions with AllowAnonymous attribute.
                var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(authorizationPolicy));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseNodeModules(env);
            }
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}