using System.Collections.Generic;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using log4net;

namespace Horn.Core
{
    public class NAntBuildTool : IBuildTool
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MSBuildBuildTool));

        public void Build(string pathToBuildFile, List<string> tasks, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }
    }
}
