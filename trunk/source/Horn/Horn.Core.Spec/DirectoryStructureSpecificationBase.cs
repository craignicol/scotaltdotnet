using System;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Spec.Framework.Extensions;

namespace Horn.Core.Spec.Unit
{
    public abstract class DirectoryStructureSpecificationBase : Specification
    {
        private const string TEST_BUILD_FILE_NAME = "build.boo";

        protected string root;
        protected DirectoryInfo rootDirectory;

        protected override void Before_each_spec()
        {
            CreateTempDirectory();
            string sourceBuildFile = CreateSourceBuildFile();

            PackageTree.CreateDefaultTreeStructure(root, sourceBuildFile);
        }

        private string CreateSourceBuildFile()
        {
            string pathToBuildConfigurationFiles = GetPathToBuildConfigurationFiles();

            return Path.Combine(pathToBuildConfigurationFiles, TEST_BUILD_FILE_NAME);
        }

        private string GetPathToBuildConfigurationFiles()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToLower().ResolvePath(), "BuildConfigs\\Horn");
        }

        private void CreateTempDirectory()
        {
            root = Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString());

            rootDirectory = new DirectoryInfo(root);
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