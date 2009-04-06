using System.Collections.Generic;
using System.IO;
using System.Threading;
using Horn.Domain.PackageStructure;

namespace Horn.Domain.SCM
{
    public abstract class SourceControl
    {

        private static Dictionary<string, string> downloadedPackages = new Dictionary<string, string>();
        protected  IDownloadMonitor downloadMonitor;


        public IDownloadMonitor DownloadMonitor
        {
            get { return downloadMonitor; }
        }

        public abstract string Revision { get; }

        public string Url {get; private set;}

        public static void ClearDownLoadedPackages()
        {
            downloadedPackages.Clear();
        }


        protected abstract void Initialise(IPackageTree packageTree);

        protected abstract string Download(DirectoryInfo destination);



        //public static T Create<T>(string url) where T : SourceControl
        //{
        //    var sourceControl = IoC.Resolve<T>();

        //    sourceControl.Url = url;

        //    return sourceControl;
        //}



        public virtual void Export(IPackageTree packageTree)
        {
            if (downloadedPackages.ContainsKey(packageTree.Name))
                return;

            if (!packageTree.GetRevisionData().ShouldUpdate(new RevisionData(Revision)))
                return;

            Initialise(packageTree);

            SetMonitor(packageTree.WorkingDirectory.FullName);

            Thread monitoringThread = StartMonitoring();

            var revision = Download(packageTree.WorkingDirectory);

            StopMonitoring(monitoringThread);

            RecordCurrentRevision(packageTree, revision);

            downloadedPackages.Add(packageTree.Name, packageTree.Name);
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

        protected SourceControl(string url)
        {
            Url = url;
        }

        protected SourceControl()
        {
        }



    }
}