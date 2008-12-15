using System;
using System.IO;
using System.Threading;
using log4net;
using SharpSvn;

namespace Horn.Core.SCM
{
    public class SVNSourceControl : SourceControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (SVNSourceControl));

        protected override void Initialise(string destination)
        {
            if (Directory.Exists(destination))
            {
                Directory.Delete(destination, true);
            }
        }

        protected override void Download(string destination)
        {
            using (var client = new SvnClient())
            {
                try
                {
                    client.Export(Url, destination);
                }
                catch (SvnObstructedUpdateException ex)
                {
                    downloadMonitor.StopMonitoring = true;

                    log.Error(ex);
                    throw;
                }
            }
        }

        protected override void SetMonitor(string destination)
        {
            downloadMonitor = new DownloadMonitor(destination);
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