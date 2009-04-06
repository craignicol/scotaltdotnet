using System.Diagnostics;

namespace Horn.Domain.BuildEngines
{
    public interface IProcess
    {
        string GetLineOrOutput();

        void WaitForExit();
    }
}