using System;
using Horn.Domain.Framework;
using Horn.Domain.PackageStructure;

namespace Horn.Domain.Spec.BuildEngine
{
    public class BuildToolStub : IBuildTool
    {
        public string PathToBuildFile { get; private set; }

        public string CommandLineArguments(string pathToBuildFile, BuildEngines.BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            Console.WriteLine(pathToBuildFile);
            Console.WriteLine(buildEngine);
            Console.WriteLine(packageTree);
            Console.WriteLine(version);

            return string.Empty;
        }

        public string PathToBuildTool(IPackageTree packageTree, FrameworkVersion version)
        {
            Console.WriteLine(version);

            return string.Empty;
        }

        public string GetFrameworkVersionForBuildTool(FrameworkVersion version)
        {
            Console.WriteLine(version);

            return "3.5";
        }
    }

}