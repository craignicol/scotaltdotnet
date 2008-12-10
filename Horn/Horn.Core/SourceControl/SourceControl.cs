using System.Threading;

namespace Horn.Core.SCM
{
    public abstract class SourceControl
    {
        protected  IDownloadMonitor downloadMonitor;

        protected abstract void Initialise(string destination);

        protected abstract void Download(string destination);

        public string Url {get; private set;}

        public IDownloadMonitor DownloadMonitor
        {
            get { return downloadMonitor; }
        }

        public virtual void Export(string destination)
        {
            Initialise(destination);

            SetMonitor(destination);

            var thread = new Thread(downloadMonitor.StartMonitoring);

            thread.Start();

            Download(destination);

            downloadMonitor.StopMonitoring = true;

            thread.Join();

        }

        protected virtual void SetMonitor(string destination)
        {
            downloadMonitor = new DefaultDownloadMonitor();
        }

        public static T Create<T>(string url) where T : SourceControl
        {
            var sourceControl = IoC.Resolve<T>();

            sourceControl.Url = url;

            return sourceControl;
        }

        protected SourceControl(string url)
        {
            Url = url;
        }

        protected SourceControl()
        {
        }
    }
}