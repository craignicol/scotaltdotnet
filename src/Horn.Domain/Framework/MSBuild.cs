using System.IO;

namespace Horn.Domain.Framework
{
    public class MSBuild
    {
        public string AssemblyPath { get; private set; }

        public MSBuild(string frameworkPath)
        {
            AssemblyPath = Path.Combine(frameworkPath, "MSBuild.exe");
        }
    }

}