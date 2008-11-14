using System;
using System.IO;

namespace Horn.Core.Spec.Unit
{
    public abstract class DirectoryStructureSpecificationBase : Specification
    {
        private const string TEST_BUILD_FILE_NAME = "build.boo";

        protected string root;
        protected DirectoryInfo rootDirectory;

        protected override void Before_each_spec()
        {
            root = string.Format("{0}\\{1}\\", AppDomain.CurrentDomain.BaseDirectory, Environment.UserName);

            CreateDirectory(root);

            var distros = string.Format("{0}{1}\\", root, "distros");

            CreateDirectory(distros);

            var horn = string.Format("{0}{1}\\", distros, "horn");

            CreateDirectory(horn);

            var destinationBuildFile = string.Format("{0}{1}", horn, TEST_BUILD_FILE_NAME);

            if (File.Exists(destinationBuildFile))
                return;

            var sourceBuildFile = string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("bin\\debug", ""), "BuildConfigs\\Horn", TEST_BUILD_FILE_NAME);

            if (!File.Exists(sourceBuildFile))
                throw new FileNotFoundException("Test horn boo file does not exist");

            File.Copy(sourceBuildFile, destinationBuildFile);
        }

        private void CreateDirectory(string root)
        {
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
        }        
    }
}