using System;
using Horn.Core.BuildEngines;

namespace Horn.Core.Spec.BuildEngine
{
    public class StubProcessFactory : IProcessFactory 
    {
        public IProcess GetProcess(string pathToBuildTool, string cmdLineArguments, string workingDirectoryPath)
        {
            return new StubProcess();
        }

        public void ExcuteCommand(string command, string workingDirectory)
        {
            Console.WriteLine(command);

            Console.WriteLine(workingDirectory);
        }
    }
}