using System;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.Extensions;

namespace Horn.Core.Spec.Unit
{
    public abstract class DirectoryStructureSpecificationBase : Specification
    {
        private const string TEST_BUILD_FILE_NAME = "build.boo";

        protected string root;
        protected DirectoryInfo rootDirectory;

        protected override void Before_each_spec()
        {
            root = string.Format("{0}\\{1}\\", Environment.GetEnvironmentVariable("temp"), Guid.NewGuid());

            rootDirectory = new DirectoryInfo(root);

            var sourceBuildFile = string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory.ToLower().ResolvePath(), "BuildConfigs\\Horn", TEST_BUILD_FILE_NAME);

            PackageTree.CreateDefaultTreeStructure(root, sourceBuildFile);
        }

        protected override void After_each_spec()
        {
            base.After_each_spec();

            if (!rootDirectory.Exists)
                return;

            try
            {
                rootDirectory.Delete(true);
            }
            catch (IOException)
            {
                RecursiveDelete(rootDirectory.FullName);
            }
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