using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace BaGet.Tests
{

    public class EmbeddedFileProviderTest
    {
        protected readonly ITestOutputHelper Helper;
        public EmbeddedFileProviderTest(ITestOutputHelper helper)
        {
            Helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        [Fact]
        public void ReturnIndexFileFromRoot()
        {
            var provider = BaGet.UI.Embedded.FileProvider.Create();
            var indexFile = provider.GetFileInfo("/index.html");
            Assert.True(indexFile.Exists);
            Assert.False(indexFile.IsDirectory);
        }

        [Fact]
        public void EnumerateRootFolder()
        {
            var provider = BaGet.UI.Embedded.FileProvider.Create();
            var content = provider.GetDirectoryContents("");
            Assert.True(content.Exists);
            Assert.True(content.Count() > 10,"currently we should have more then 10 files for the client, in multiple subdirectories");
        }



    }

}
