using System;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BaGet.UI
{

    public static class BaGetUIExtensions
    {

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
