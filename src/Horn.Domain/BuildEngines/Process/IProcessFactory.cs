namespace Horn.Domain.BuildEngines
{
    public interface IProcessFactory
    {
        IProcess GetProcess(string pathToBuildTool, string cmdLineArguments, string workingDirectoryPath);
    }
}