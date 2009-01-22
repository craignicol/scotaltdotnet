using System.IO;
using Horn.Core.PackageStructure;
using Horn.Framework.helpers;

namespace Horn.Core.Spec.Unit
{
    public abstract class DirectoryStructureSpecificationBase : Specification
    {
        protected string root;
        protected DirectoryInfo rootDirectory;

        protected override void Before_each_spec()
        {
            CreateTempDirectory();

            root = rootDirectory.FullName;

            PackageTreeHelper.CreatePackageTreeForTesting(root);
        }

        private void CreateTempDirectory()
        {
            rootDirectory = new DirectoryInfo(DirectoryHelper.GetTempDirectoryName());
        }
    }
}