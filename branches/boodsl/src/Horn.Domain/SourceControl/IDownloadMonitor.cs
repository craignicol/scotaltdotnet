namespace Horn.Domain.SCM
{
    public interface IDownloadMonitor
    {
        bool StopMonitoring { get; set; }

        void StartMonitoring();
    }
}