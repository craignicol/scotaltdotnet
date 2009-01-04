using System.Diagnostics;

namespace Horn.Core.BuildEngines
{
    public class DiagnosticsProcessFactory : IProcessFactory
    {
        public IProcess GetProcess(string pathToBuildTool, string cmdLineArguments, string workingDirectoryPath)
        {
            var psi = new ProcessStartInfo(pathToBuildTool, cmdLineArguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WorkingDirectory = workingDirectoryPath
            };  

            return new DiagnosticsProcess(Process.Start(psi));
        }
    }
}
