using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using log4net;

namespace Horn.Core
{
    public class NAntBuildTool : IBuildTool
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MSBuildBuildTool));

        private string cmdLineArguments;


        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            return string.Format(" -t:net-{0} -buildfile:{1} {2}",  GetFrameworkVersion(version), pathToBuildFile, GenerateParameters(buildEngine.Parameters));
        }

        public void Build(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            cmdLineArguments = CommandLineArguments(pathToBuildFile, buildEngine, packageTree, version);
                                     
            var pathToNant = Path.Combine(packageTree.WorkingDirectory.FullName, @"lib\net\NAnt.Core.dll");
        }

        private string GenerateParameters(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Keys.Count == 0)
                return string.Empty;

            var stringBuilder = new StringBuilder();

            foreach(var key in parameters.Keys)
               stringBuilder.AppendFormat("-D:{0}={1} ", key, parameters[key]);

            return stringBuilder.ToString();
        }

        private string GenerateTasks(List<string> tasks)
        {
            if (tasks == null || tasks.Count == 0)
                return string.Empty;

            var ret = "";

            tasks.ForEach(x => ret += string.Format("{0} ", x));

            return ret;
        }

        private string GetFrameworkVersion(FrameworkVersion version)
        {
            switch(version)
            {
                case FrameworkVersion.frameworkVersion2:
                    return "2.0";
                case FrameworkVersion.frameworkVersion35:
                    return "3.5";
            }
            
            throw new InvalidEnumArgumentException("Invalid Framework version paased to NAntBuildTool.GetFrameworkVersion", (int)version, typeof(FrameworkVersion));
        }
    }
}
 