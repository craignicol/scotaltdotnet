using System;
using System.IO;
using Horn.Core.extensions;
using Xunit;

namespace Horn.Core.Spec.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void CopyToDirectory(this DirectoryInfo source, DirectoryInfo destination)
        {
            if(destination.Exists)
                destination.Delete(true);    

            destination.Create();

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(Path.GetDirectoryName(destination.FullName), Path.GetFileName(file.FullName)), true);
            }

            foreach (var dir in source.GetDirectories())
            {
                if(dir.FullName.Contains(".svn"))
                    continue;

                var newDirectory = new DirectoryInfo(Path.Combine(destination.FullName, dir.Name));

                dir.CopyToDirectory(newDirectory);
            }
        }
    }

    public class When_Copying_From_A_Folder : Specification
    {
        private DirectoryInfo source;
        private DirectoryInfo destination;

        protected override void Because()
        {
            source = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.RemoveDebugFolderParts()).Parent;

            destination = new DirectoryInfo(Path.Combine(new DirectoryInfo("C:\\").FullName, "Working"));

            source.CopyToDirectory(destination);
        }

        [Fact]
        public void Then_SubFolders_And_Files_Are_Copied()
        {
            Assert.True(destination.GetDirectories().Length > 0);

            Assert.True(destination.GetFiles().Length > 0);            
        }
    }
}