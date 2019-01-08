using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders.Embedded;
using Microsoft.Extensions.FileProviders;
namespace BaGet.UI.Embedded
{
    public static class FileProvider
    {
        public const string BaseNamespace = "BaGet.UI.Embedded";

        public static System.Reflection.Assembly ResourceAssembly = typeof(FileProvider).Assembly;

        public static IFileProvider Create()
        {
            return new EmbeddedFileProvider(ResourceAssembly, BaseNamespace);
        }
    }
}
