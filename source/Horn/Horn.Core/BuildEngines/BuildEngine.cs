namespace Horn.Core
{
    using System.Collections.Generic;

    public abstract class BuildEngine
    {
        public virtual void AssignTasks(string[] tasks)
        {
            Tasks = new List<string>(tasks);
        }

        public virtual string BuildFile { get; private set; }

        public virtual List<string> Tasks { get; private set; }

        protected BuildEngine(string buildFile)
        {
            BuildFile = buildFile;
        }
    }
}