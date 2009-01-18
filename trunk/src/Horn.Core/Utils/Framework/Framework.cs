using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using log4net;

namespace Horn.Core.Utils.Framework
{
    public enum FrameworkVersion
    {
        frameworkVersion2,
        frameworkVersion35
    }

    public class Framework
    {
        private static readonly IDictionary<FrameworkVersion, string> assemblyPaths = new Dictionary<FrameworkVersion, string>();

        private static readonly ILog log = LogManager.GetLogger(typeof (Framework));

        public FrameworkVersion Version { get; private set; }

        public MSBuild MSBuild
        {
            get { return new MSBuild(assemblyPaths[Version]); }
        }

        public Framework(FrameworkVersion version)
        {
            Version = version;
        }

        static Framework()
        {
            //HACK: Is there a better way to determine the Correct framework path
            var currentVersion = RuntimeEnvironment.GetRuntimeDirectory();

            Console.WriteLine("Runtime directory = {0}", RuntimeEnvironment.GetRuntimeDirectory());

            string root;
            
            if(currentVersion.IndexOf("Framework64") > 0)
                root = currentVersion.Substring(0, currentVersion.LastIndexOf("\\Framework64\\") + "\\Framework64\\".Length);
            else
                root = currentVersion.Substring(0, currentVersion.LastIndexOf("\\Framework\\") + "\\Framework\\".Length);

            assemblyPaths.Add(FrameworkVersion.frameworkVersion2, Path.Combine(root, "v2.0.50727"));
            assemblyPaths.Add(FrameworkVersion.frameworkVersion35, Path.Combine(root, "v3.5"));
        }
    }
}