using System.IO;
using System.Threading;
using Horn.Core.PackageStructure;

namespace Horn.Core.SCM
{
    public abstract class SourceControl
    {
        public abstract string Revision { get; }

        protected  IDownloadMonitor downloadMonitor;

        protected abstract void Initialise(IPackageTree packageTree);

        protected abstract string Download(DirectoryInfo destination);

        public string Url {get; private set;}

        public IDownloadMonitor DownloadMonitor
        {
            get { return downloadMonitor; }
        }

        public virtual void Export(IPackageTree packageTree)
        {
            if (!packageTree.GetRevisionData().ShouldUpdate(new RevisionData(Revision)))
                return;

            Initialise(packageTree);

            SetMonitor(packageTree.WorkingDirectory.FullName);

            Thread monitoringThread = StartMonitoring();

            var revision = Download(packageTree.WorkingDirectory);

            StopMonitoring(monitoringThread);

            RecordCurrentRevision(packageTree, revision);
        }

        protected virtual void RecordCurrentRevision(IPackageTree tree, string revision)
        {
            tree.GetRevisionData().RecordRevision(tree, revision);
        }

        protected virtual void StopMonitoring(Thread thread)
        {
            downloadMonitor.StopMonitoring = true;

            thread.Join();
        }

        protected virtual Thread StartMonitoring()
        {
            var thread = new Thread(downloadMonitor.StartMonitoring);

            thread.Start();

            return thread;
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