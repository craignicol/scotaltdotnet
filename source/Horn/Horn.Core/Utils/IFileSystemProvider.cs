namespace Horn.Core.Utils
{
    using System.IO;

    /// <summary>
    /// Basic wrapper for the file system to aid testing.  We don't want to hit the file system in the unit tests.
    /// Keep that for the integration tests
    /// </summary>
    public interface IFileSystemProvider
    {
        string CreateDirectory(string path);
    }

    public class FileSystemProvider : IFileSystemProvider
    {
        public string CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path).FullName;
        }
    }
}