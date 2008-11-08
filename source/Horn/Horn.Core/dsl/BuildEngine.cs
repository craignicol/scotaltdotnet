using System.Collections.Generic;

namespace Horn.Core.dsl
{
    public abstract class BuildEngine
    {
        protected BuildEngine(string buildFile)
        {
            BuildFile = buildFile;
        }

        public virtual void AssignTasks(string[] tasks)
        {
            Tasks = new List<string>(tasks);
        }

        public virtual string BuildFile { get; private set; }

        public virtual List<string> Tasks { get; private set; }
    }

    public class NAntBuildEngine : BuildEngine
    {
        public NAntBuildEngine(string buildFile) : base(buildFile)
        {
        }
    }

    public class RakeBuildEngine : BuildEngine
    {
        public RakeBuildEngine(string buildFile) : base(buildFile)
        {
        }
    }
}