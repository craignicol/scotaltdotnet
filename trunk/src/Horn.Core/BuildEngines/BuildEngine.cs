using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Utils;
using Horn.Core.Utils.Framework;

namespace Horn.Core.BuildEngines
{
    public class BuildEngine
    {
        public string BuildFile { get; private set; }

        public FrameworkVersion Version { get; private set; }

        public List<string> Tasks { get; private set; }

        public IBuildTool BuildTool { get; private set; }

        public Dictionary<string, string> Parameters{ get; private set;}

        public void AssignParameters(string[] parameters)
        {
            if ((parameters == null) || (parameters.Length == 0))
                return;

            Parameters = new Dictionary<string, string>();

            parameters.ForEach(x =>
                                   {
                                       var parts = x.Split('=');
                                        
                                       Parameters.Add(parts[0], parts[1]);
                                   });
        }

        public virtual void AssignTasks(string[] tasks)
        {
            Tasks = new List<string>(tasks);
        }

        public virtual BuildEngine Build(IPackageTree tree)
        {
            string buildFilePath = GetBuildFilePath(tree);

            BuildTool.Build(buildFilePath, this, tree, Version);

            return this;
        }

        private string GetBuildFilePath(IPackageTree tree)
        {
            return Path.Combine(tree.WorkingDirectory.FullName, BuildFile).Replace('/', '\\');
        }

        public BuildEngine(IBuildTool buildTool, string buildFile, FrameworkVersion version)
        {
            BuildTool = buildTool;

            BuildFile = buildFile;

            Version = version;
        }
    }
}
