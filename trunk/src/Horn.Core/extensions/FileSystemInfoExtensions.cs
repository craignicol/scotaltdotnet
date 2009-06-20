using System.Collections.Generic;
using System.IO;

namespace Horn.Core.extensions
{
    public static class FileSystemInfoExtensions
    {
        public static void CopyToDirectory(this DirectoryInfo source, DirectoryInfo destination)
        {
            if (destination.Exists)
                destination.Delete(true);

            destination.Create();

            CopyFiles(source, destination);

            CopyDirectories(source, destination);
        }

        private static void CopyDirectories(DirectoryInfo source, DirectoryInfo destination)
        {
            foreach (var dir in source.GetDirectories())
            {
                if (dir.FullName.Contains(".Svn"))
                    continue;

                var newDirectory = new DirectoryInfo(Path.Combine(destination.FullName, dir.Name));

                dir.CopyToDirectory(newDirectory);
            }
        }

        private static void CopyFiles(DirectoryInfo source, DirectoryInfo destination)
        {
            foreach (var file in source.GetFiles())
            {
                var destinationFile = Path.Combine(destination.FullName, Path.GetFileName(file.FullName));

                file.CopyTo(destinationFile, true);
            }
        }

        public static FileSystemInfo GetExportPath(string fullPath)
        {
            FileSystemInfo exportPath;

            if (fullPath.PathIsFile())
            {
                return new FileInfo(fullPath);
            }

            exportPath = new DirectoryInfo(fullPath);

            if (!exportPath.Exists)
            {
                var directoryInfo = (DirectoryInfo)exportPath;
                directoryInfo.Create();
            }

            return exportPath;
        }

        public static FileSystemInfo GetFileSystemObjectFromParts(this DirectoryInfo sourceDirectory, string parts)
        {
            if (string.IsNullOrEmpty(parts))
                return sourceDirectory;

            var outputPath = sourceDirectory.FullName;

            if (parts.Trim() == ".")
                return sourceDirectory;

            foreach (var part in parts.Split('/'))
            {
                outputPath = Path.Combine(outputPath, part);
            }

            return outputPath.PathIsFile() ? (FileSystemInfo)new FileInfo(outputPath) : new DirectoryInfo(outputPath);
        }

        public static IEnumerable<string> Search(this DirectoryInfo root, string searchPattern)
        {
            var dirs = new Queue<string>();
            dirs.Enqueue(root.FullName);

            var results = new List<string>();
            while (dirs.Count > 0)
            {
                SearchDirectories(searchPattern, dirs, results);
            }

            return results;
        }

        private static void SearchDirectories(string searchPattern, Queue<string> directories, List<string> results)
        {
            string dir = directories.Dequeue();

            string[] filePaths = null;

            filePaths = AddFiles(dir, searchPattern, filePaths, results);

            string[] directoryPaths = null;

            try
            {
                directoryPaths = Directory.GetDirectories(dir);
            }
            catch
            {
            }

            if (directoryPaths != null && directoryPaths.Length > 0)
            {
                foreach (string subDir in directoryPaths)
                {
                    directories.Enqueue(subDir);
                }
            }
        }

        private static string[] AddFiles(string dir, string searchPattern, string[] filePaths, List<string> results)
        {
            // I do not like swallowing exceptions but there is a good reason
            // http://msdn.microsoft.com/en-us/library/bb513869.aspx
            try
            {
                filePaths = Directory.GetFiles(dir, searchPattern);
            }
            catch
            {
            }

            if (filePaths != null && filePaths.Length > 0)
            {
                foreach (string file in filePaths)
                {
                    results.Add(file);
                }
            }

            return filePaths;
        }
    }
}
