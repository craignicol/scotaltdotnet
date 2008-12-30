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
    }

    public class StubProcess : IProcess
    {
        public string GetLineOrOutput()
        {
            return "This is from the fake process";
        }

        public void WaitForExit()
        {
            Console.WriteLine("WaitForExit called in the StubProcess");
        }
    }
}