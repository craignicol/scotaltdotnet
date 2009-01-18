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

        private void RecursiveDelete(string path)
        {
            if (!Directory.Exists(path))
                return;

            var directory = new DirectoryInfo(path);

            try
            {
                foreach (var child in directory.GetDirectories())
                    RecursiveDelete(child.FullName);

                if (directory.Exists)
                    directory.Delete(true);
            }
            catch (UnauthorizedAccessException ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}