namespace Horn.Core
{
    using System.Collections.Generic;

    public class BuildEngine
    {
        public virtual void AssignTasks(string[] tasks)
        {
            Tasks = new List<string>(tasks);
        }

        public virtual string BuildFile { get; private set; }

        public virtual List<string> Tasks { get; private set; }

        public IBuildTool BuildTool { get; private set; }

        public BuildEngine(IBuildTool buildTool, string buildFile)
        {
            BuildTool = buildTool;

            BuildFile = buildFile;
        }
    }
}