using System;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace BaGet.UI
{
    public class EmbeddedResourceSpaStaticFileProvider : ISpaStaticFileProvider
    {
        private readonly IFileProvider _fileProvider;

        public EmbeddedResourceSpaStaticFileProvider(
            IServiceProvider serviceProvider,
            BaGetUIOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.EmbeddedResourceAssembly == null)
            {
                throw new ArgumentException($"The {nameof(options.EmbeddedResourceAssembly)} property " +
                    $"of {nameof(options)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(options.BaseNamespace))
            {
                _fileProvider = new EmbeddedFileProvider(options.EmbeddedResourceAssembly);
            }
            else
            {
                _fileProvider = new EmbeddedFileProvider(options.EmbeddedResourceAssembly, options.BaseNamespace);
            }
        }

        public IFileProvider FileProvider => _fileProvider;
    }
}
