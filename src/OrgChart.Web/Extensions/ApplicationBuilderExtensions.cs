using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace OrgChart.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (env == null) throw new ArgumentNullException(nameof(env));
            var path = Path.Combine(env.ContentRootPath, "node_modules");
            var provider = new PhysicalFileProvider(path);
            var options = new FileServerOptions { RequestPath = "/node_modules" };
            options.StaticFileOptions.FileProvider = provider;
            options.EnableDirectoryBrowsing = false;
            app.UseFileServer(options);
            return app;
        }
    }
}
