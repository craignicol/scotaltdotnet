using Horn.Domain.BuildEngines;
namespace Horn.Domain.Spec.BuildEngine
{
    public class StubProcessFactory : IProcessFactory 
    {
        public IProcess GetProcess(string pathToBuildTool, string cmdLineArguments, string workingDirectoryPath)
        {
            return new StubProcess();
        }
    }
}