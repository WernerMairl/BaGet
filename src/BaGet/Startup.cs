using System;
using BaGet.Configurations;
using BaGet.Core.Configuration;
using BaGet.Core.Entities;
using BaGet.Extensions;
using BaGet.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BaGet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureBaGet(Configuration, httpServices: true);

            //Load UI from a pure amd clean dotnet core assembly (client files as embedded resource)
            services.AddBaGetDefaultUI((configuration) =>
            {
               configuration.BaseNamespace = "MyRoot.build";
               configuration.EmbeddedResourceAssembly = typeof(BaGet.UI.BaGetUIOptions).Assembly;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            // Run migrations if necessary.
            var options = Configuration.Get<BaGetOptions>();
            if (options.RunMigrationsAtStartup)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider
                        .GetRequiredService<IContext>()
                        .Database
                        .Migrate();
                }
            }

            app.UsePathBase(options.PathBase);

            app.UseForwardedHeaders();

            app.UseCors(ConfigureCorsOptions.CorsPolicy);

            app.UseMvc(routes =>
            {
                routes
                    .MapServiceIndexRoutes()
                    .MapPackagePublishRoutes()
                    .MapSymbolRoutes()
                    .MapSearchRoutes()
                    .MapRegistrationRoutes()
                    .MapPackageContentRoutes();
            });

            app.UseBaGetDefaultUI();

        }
    }
}
