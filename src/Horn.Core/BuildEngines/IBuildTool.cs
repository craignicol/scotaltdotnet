using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public interface IBuildTool
    {
        string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version);

        void Build(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version);
    }
}
