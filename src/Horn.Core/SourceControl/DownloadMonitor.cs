using System.IO;
using System.Threading;
using log4net;

namespace Horn.Core.SCM
{
    public class DownloadMonitor : IDownloadMonitor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (DownloadMonitor));

        private readonly string downloadDirectory;

        public bool StopMonitoring { get; set; }

        public void StartMonitoring()
        {
            StopMonitoring = false;

            while ((!Directory.Exists(downloadDirectory) && (!StopMonitoring)))
            {
                Thread.Sleep(10);
            }

            var watcher = new FileSystemWatcher(downloadDirectory)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            var file = Path.GetFileName(e.FullPath);

            var dir = Path.GetDirectoryName(e.FullPath);

            if (!string.IsNullOrEmpty(file) && (file.IndexOf("tmp") > -1))
                return;

            if (!string.IsNullOrEmpty(dir) && (dir.IndexOf("tmp") > -1))
                return;

            log.InfoFormat("{0} was {1} in {2}", file, e.ChangeType, dir);
        }

        public DownloadMonitor(string downloadDirectory)
        {
            this.downloadDirectory = downloadDirectory;
        }
    }
}