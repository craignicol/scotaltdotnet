using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public class NAntBuildTool : IBuildTool
    {
        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            return string.Format(" -t:net-{0} -buildfile:{1} {2}",  GetFrameworkVersionForBuildTool(version), pathToBuildFile, GenerateParameters(buildEngine.Parameters));
        }

        public string PathToBuildTool(IPackageTree packageTree, FrameworkVersion version)
        {
            return Path.Combine(packageTree.WorkingDirectory.FullName, @"lib\Nant\NAnt.exe");
        }

        public string GetFrameworkVersionForBuildTool(FrameworkVersion version)
        {
            switch (version)
            {
                case FrameworkVersion.FrameworkVersion2:
                    return "2.0";
                case FrameworkVersion.FrameworkVersion35:
                    return "3.5";
            }

            throw new InvalidEnumArgumentException("Invalid Framework version paased to NAntBuildTool.GetFrameworkVersion", (int)version, typeof(FrameworkVersion));
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
    }
}
 