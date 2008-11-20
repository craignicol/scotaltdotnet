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
            root = string.Format("{0}\\{1}\\", AppDomain.CurrentDomain.BaseDirectory, Environment.UserName);

            var sourceBuildFile = string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory.ToLower().ResolvePath(), "BuildConfigs\\Horn", TEST_BUILD_FILE_NAME);

            PackageTree.CreateDefaultTreeStructure(root, sourceBuildFile);
        }       
    }
}