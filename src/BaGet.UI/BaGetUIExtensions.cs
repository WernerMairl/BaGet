using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BaGet.UI
{


    public static class BaGetUIExtensions
    {
        public static void UseBaGetDefaultUI(this IApplicationBuilder app)//, Action<ISpaBuilder> configuration)
        {
            //if (configuration == null)
            //{
            //    throw new ArgumentNullException(nameof(configuration));
            //}

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                //    //spa.Options.SourcePath = "../BaGet.UI";
                //    //if (env.IsDevelopment())
                //    //{
                //    //    spa.UseReactDevelopmentServer(npmScript: "start");
                //    //}
            });
        }


        public static void AddBaGetDefaultUI(
            this IServiceCollection services,
            Action<BaGetUIOptions> configuration = null)
        {
            services.AddSingleton<ISpaStaticFileProvider>(serviceProvider =>
            {
                var optionsProvider = serviceProvider.GetService<IOptions<BaGetUIOptions>>();
                var options = optionsProvider.Value;
                // Allow the developer to perform further configuration
                configuration?.Invoke(options);
                return new EmbeddedResourceSpaStaticFileProvider(serviceProvider, options);
            });
        }

    }
}
