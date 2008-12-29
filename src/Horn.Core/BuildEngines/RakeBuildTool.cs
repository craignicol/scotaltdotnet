using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public class RakeBuildTool : IBuildTool
    {
        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }

        public void Build(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }
    }
}
