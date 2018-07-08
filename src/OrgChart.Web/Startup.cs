using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrgChart.Core.Interfaces;
using OrgChart.Data;
using OrgChart.Infrastructure.Identity;
using OrgChart.Web.Extensions;

namespace OrgChart.Web
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseInMemoryDatabase("Identity"));
                services.AddDbContext<OrgChartDbContext>(options => options.UseInMemoryDatabase("OrgChart"));
            }

            if (_hostingEnvironment.IsProduction())
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(connectionString));
                services.AddDbContext<OrgChartDbContext>(options => options.UseSqlServer(connectionString));
            }

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

        public void Configure(IApplicationBuilder app)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseNodeModules(_hostingEnvironment);
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}