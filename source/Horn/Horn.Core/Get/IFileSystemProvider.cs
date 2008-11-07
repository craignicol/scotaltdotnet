namespace Horn.Core.Get
{
    /// <summary>
    /// Basic wrapper for the file system to aid testing.  We don't want to hit the file system in the unit tests.
    /// Keep that for the integration tests
    /// </summary>
    public interface IFileSystemProvider
    {
        void CreateDirectory(string path);
    }
}