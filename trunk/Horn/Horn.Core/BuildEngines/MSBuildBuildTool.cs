using System;
using System.Diagnostics;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using log4net;

namespace Horn.Core
{
    public class MSBuildBuildTool : IBuildTool
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (MSBuildBuildTool));

        public void Build(string pathToBuildFile, IPackageTree packageTree, FrameworkVersion version)
        {
            var pathToMsBuild = FrameworkLocator.Instance[version].MSBuild.AssemblyPath;

            var args =
                string.Format(
                    "\"{0}\" /p:OutputPath=\"{1}\"  /p:TargetFrameworkVersion={2} /p:NoWarn=1591 /consoleloggerparameters:Summary",
                    pathToBuildFile, packageTree.OutputDirectory, GetCmdLineFrameworkVersion(version));

            var psi = new ProcessStartInfo(pathToMsBuild, args)
                                       {
                                           UseShellExecute = false,
                                           RedirectStandardOutput = true,
                                           WorkingDirectory = packageTree.WorkingDirectory.FullName
                                       };

            var msBuild = Process.Start(psi);

            while (true)
            {
                var line = msBuild.StandardOutput.ReadLine();

                if (line == null)
                    break;

                log.Info(line);
            }

            msBuild.WaitForExit();
        }

        private string GetCmdLineFrameworkVersion(FrameworkVersion version)
        {
            switch(version)
            {
                case FrameworkVersion.frameworkVersion2:
                    return "v2.0";
                case FrameworkVersion.frameworkVersion35:
                    return "v3.5";
                default:
                    throw new ArgumentException(string.Format("Unknown framework version: {0}", version));
            }
        }
    }
}
