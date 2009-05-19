using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Horn.Core.extensions;
using Horn.Core.PackageStructure;

namespace Horn.Core.SCM
{
    public abstract class SourceControl
    {

        private static Dictionary<string, string> downloadedPackages = new Dictionary<string, string>();

        private static readonly object locker = new object();

        protected  IDownloadMonitor downloadMonitor;


        public IDownloadMonitor DownloadMonitor
        {
            get { return downloadMonitor; }
        }

        public string ExportPath { get; private set; }

        public abstract string Revision { get; }

        public string Url {get; private set;}

        public static void ClearDownLoadedPackages()
        {
            downloadedPackages.Clear();
        }


        protected abstract void Initialise(IPackageTree packageTree);

        protected abstract string Download(FileSystemInfo destination);



        public static T Create<T>(string url) where T : SourceControl
        {
            var sourceControl = IoC.Resolve<T>();

            sourceControl.Url = url;

            return sourceControl;
        }



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

        public virtual void Export(IPackageTree packageTree, string path, bool initialise)
        {
            lock (locker)
            {
                if (initialise)
                    Initialise(packageTree);

                downloadMonitor = new DefaultDownloadMonitor();

                var fullPath = Path.Combine(packageTree.WorkingDirectory.FullName, path);

                FileSystemInfo exportPath = GetExportPath(fullPath);

                Thread monitoringThread = StartMonitoring();

                Download(exportPath);

                StopMonitoring(monitoringThread);
            }
        }

        protected virtual FileSystemInfo GetExportPath(string fullPath)
        {
            FileSystemInfo exportPath;

            if (!fullPath.PathIsFile()) 
            {
                exportPath = new DirectoryInfo(fullPath);

                if (!exportPath.Exists)
                    ((DirectoryInfo)exportPath).Create();                    
            }
            else
            {
                exportPath = new FileInfo(fullPath);
            }
            return exportPath;
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

        protected SourceControl(string url, string exportPath)
        {
            Url = url;
            ExportPath = (string.IsNullOrEmpty(exportPath) ? "" : exportPath);
        }


        protected SourceControl()
        {
        }



    }
}