using System.IO;

using Horn.Framework.helpers;

namespace Horn.Domain.Spec
{
    public abstract class DirectorySpecificationBase : Specification
    {
        protected DirectoryInfo rootDirectory;

        protected override void Before_each_spec()
        {
            rootDirectory = PackageTreeHelper.CreateDirectoryStructureForTesting();
        }
    }
}