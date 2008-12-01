using System.IO;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    using System.Collections.Generic;

    public class BuildEngine
    {
        public virtual void AssignTasks(string[] tasks)
        {
            Tasks = new List<string>(tasks);
        }

        public string BuildFile { get; private set; }

        public FrameworkVersion Version { get; private set; }

        public List<string> Tasks { get; private set; }

        public IBuildTool BuildTool { get; private set; }

        public virtual BuildEngine Build(IPackageTree tree)
        {
            var buildFilePath = Path.Combine(tree.WorkingDirectory.FullName, BuildFile);

            BuildTool.Build(buildFilePath, Version);

            return this;
        }

        public BuildEngine(IBuildTool buildTool, string buildFile, FrameworkVersion version)
        {
            BuildTool = buildTool;

            BuildFile = buildFile;
            Version = version;
        }
    }
}