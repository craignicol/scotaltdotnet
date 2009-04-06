using System;
using Horn.Domain.BuildEngines;
using log4net;

namespace Horn.Domain.Spec.BuildEngine
{
    public class StubProcess : IProcess
    {
        public string GetLineOrOutput()
        {
            return null;
        }

        public void WaitForExit()
        {
            Console.WriteLine("WaitForExit called in the StubProcess");
        }
    }
}