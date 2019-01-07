using System.Reflection;
using System;

namespace BaGet.UI
{
    public class BaGetUIOptions
    {
        public string BaseNamespace { get; set; }
        public Assembly EmbeddedResourceAssembly { get; set; }
    }
}
