using System;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public class MSBuildBuildTool : IBuildTool
    {

        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, 
                        FrameworkVersion version)
        {
            return string.Format(
                    "\"{0}\" /p:OutputPath=\"{1}\"  /p:TargetFrameworkVersion={2} /p:NoWarn=1591 /consoleloggerparameters:Summary",
                    pathToBuildFile, packageTree.OutputDirectory, GetFrameworkVersionForBuildTool(version));
        }

        public string GetFrameworkVersionForBuildTool(FrameworkVersion version)
        {
            switch(version)
            {
                case FrameworkVersion.FrameworkVersion2:
                    return "v2.0";
                case FrameworkVersion.FrameworkVersion35:
                    return "v3.5";
                default:
                    throw new ArgumentException(string.Format("Unknown framework version: {0}", version));
            }
        }

        public string PathToBuildTool(IPackageTree packageTree, FrameworkVersion version)
        {
            return FrameworkLocator.Instance[version].MSBuild.AssemblyPath;
        }



    }
}
