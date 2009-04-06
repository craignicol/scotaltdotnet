using System.Diagnostics;

namespace Horn.Domain.BuildEngines
{
    public class DiagnosticsProcess : IProcess
    {

        private readonly Process process;


        public string GetLineOrOutput()
        {
            return process.StandardOutput.ReadLine();
        }

        public void WaitForExit()
        {
            process.WaitForExit();
        }



        public DiagnosticsProcess(Process process)
        {
            this.process = process;
        }



    }
}