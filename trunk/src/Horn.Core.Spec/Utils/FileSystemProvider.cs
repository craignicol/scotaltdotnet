using Horn.Framework.helpers;

namespace Horn.Core.Spec.Integration.Utils
{
    using System;
    using System.IO;
    using Core.Utils;
    using Xunit;

    public class FileSystemProviderSpec : IDisposable
    {
        readonly string path = Path.Combine(DirectoryHelper.GetBaseDirectory(), DateTime.Now.Ticks.ToString());

        [Fact]
        public void CreateDirectory_Will_Create_Directory()
        {
            CreateSUT().CreateDirectory(path);
            
            Assert.True(Directory.Exists(path));
        }

        private IFileSystemProvider CreateSUT()
        {
            return new FileSystemProvider();
        }

        public void Dispose()
        {
            Directory.Delete(path);
        }
    }
}