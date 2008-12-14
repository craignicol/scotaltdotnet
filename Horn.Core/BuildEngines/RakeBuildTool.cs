using System.Collections.Generic;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public class RakeBuildTool : IBuildTool
    {
        public void Build(string pathToBuildFile, List<string> tasks, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }
    }
}
