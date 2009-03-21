namespace Horn.Core.BuildEngines
{
    public class Dependency
    {
        public string PackageName { get; private set; }
        public string BuildFile { get; private set; }

        public Dependency(string package, string buildFile)
        {
            PackageName = package;
            BuildFile = buildFile;
        }
    }
}