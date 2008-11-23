using System.IO;
using SharpSvn;

namespace Horn.Core.SCM
{
    public class SVNSourceControl : SourceControl
    {
        public override void Export(string destination)
        {
            if(Directory.Exists(destination))
                Directory.Delete(destination);

            using(var client = new SvnClient())
            {
                client.Export(Url, destination);
            }
        }

        public SVNSourceControl(string url)
            : base(url)
        {
        }

        public SVNSourceControl()
        {
        }
    }
}