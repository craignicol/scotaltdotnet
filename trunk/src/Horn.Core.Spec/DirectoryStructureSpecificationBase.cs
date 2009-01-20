using System;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Spec.Framework.Extensions;

namespace Horn.Core.Spec.Unit
{
    public abstract class DirectoryStructureSpecificationBase : Specification
    {
        protected string root;
        protected DirectoryInfo rootDirectory;

        protected override void Before_each_spec()
        {
            CreateTempDirectory();

            PackageTree.CreateDefaultTreeStructure(root);
        }

        private void CreateTempDirectory()
        {
            root = Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString());

            rootDirectory = new DirectoryInfo(root);
        }
    }
}