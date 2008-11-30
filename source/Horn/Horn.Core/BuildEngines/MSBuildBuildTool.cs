using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using log4net;
using log4net.Core;

namespace Horn.Core
{
    public class MSBuildBuildTool : IBuildTool
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (MSBuildBuildTool));

        public static string MSBuildPath { get; private set; }

        public void Build(string pathToBuildFile)
        {
            var args =
                string.Format(
                    "\"{0}\" /p:OutputPath=\"{1}\"  /p:TargetFrameworkVersion=v3.5 /p:NoWarn=1591 /consoleloggerparameters:Summary",
                    pathToBuildFile, Path.GetDirectoryName(pathToBuildFile));

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(MSBuildPath, args)
                                           {
                                               UseShellExecute = false,
                                               RedirectStandardOutput = true,
                                               WorkingDirectory = Path.GetDirectoryName(pathToBuildFile)
                                           };

                var msBuild = Process.Start(psi);

                while (true)
                {
                    // read line
                    string line = msBuild.StandardOutput.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    // display line
                    log.Info(line);
                }

                msBuild.WaitForExit();
            }
            catch (Exception ex)
            {
                log.Error(ex);

                throw;
            }
        }

        static MSBuildBuildTool()
        {
            MSBuildPath = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "MSBuild.exe");
        }
    }
}