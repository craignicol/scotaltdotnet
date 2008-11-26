using System.IO;
using log4net;
using SharpSvn;

namespace Horn.Core.SCM
{
    public class SVNSourceControl : SourceControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (SVNSourceControl));

        public override void Export(string destination)
        {
            if(Directory.Exists(destination))
                Directory.Delete(destination);

            log.InfoFormat("Starting download of subversion source from {0}.\n", Url);

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