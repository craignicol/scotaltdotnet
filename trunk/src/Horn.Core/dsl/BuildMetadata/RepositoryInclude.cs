namespace Horn.Core.Dsl
{
    public class RepositoryInclude
    {
        public string ExportPath { get; private set; }

        public string IncludePath { get; private set; }

        public string RepositoryName { get; private set; }

        public RepositoryInclude(string repositoryName, string includePath, string exportPath)
        {
            RepositoryName = repositoryName;
            IncludePath = includePath;
            ExportPath = exportPath;
        }
    }
}