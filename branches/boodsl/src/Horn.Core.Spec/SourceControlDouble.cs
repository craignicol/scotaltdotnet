using System.IO;
using Horn.Core.SCM;
using Horn.Framework.helpers;

namespace Horn.Core.Spec
{
    public class SourceControlDouble : SVNSourceControl
    {
        public bool ExportWasCalled;

        protected override void Initialise(string destination)
        {
            System.Console.WriteLine("In initialise");
        }

        protected override void Download(string destination)
        {
            System.Console.WriteLine("In Download");
        }

        public override void Export(string destination)
        {
            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            FileHelper.CreateFileWithRandomData(Path.Combine(destination, "build.boo"));

            ExportWasCalled = true;
        }

        public SourceControlDouble(string url)
            : base(url)
        {
        }
    }
}