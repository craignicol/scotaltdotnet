using System;

namespace Horn.Core.Spec.BuildEngine
{
    using System.Collections.Generic;
    using PackageStructure;
    using Utils.Framework;
    using BuildEngines;

    public class BuildToolStub : IBuildTool
    {
        public string PathToBuildFile { get; private set; }

        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
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