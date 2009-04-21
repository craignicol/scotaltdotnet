namespace Horn.Core.extensions
{
    using System.Collections.Generic;
    using System.IO;

    internal static class DirectoryInfoExtensions
    {
        public static DirectoryInfo GetDirectoryFromParts(this DirectoryInfo sourceDirectory, string parts)
        {
            if (string.IsNullOrEmpty(parts))
                return sourceDirectory;

            string outputPath = sourceDirectory.FullName;

            if (parts.Trim() == ".")
                return sourceDirectory;

            foreach (var part in parts.Split('/'))
            {
                outputPath = Path.Combine(outputPath, part);
            }

            return new DirectoryInfo(outputPath);
        }

        public static IEnumerable<string> Search(this DirectoryInfo root, string searchPattern)
        {
            Queue<string> dirs = new Queue<string>();
            dirs.Enqueue(root.FullName);

            while (dirs.Count > 0)
            {
                string dir = dirs.Dequeue();

                string[] filePaths  = Directory.GetFiles(dir, searchPattern);

                if (filePaths != null && filePaths.Length > 0)
                {
                    foreach (string file in filePaths)
                    {
                        yield return file;
                    }
                }

                var directoryPaths = Directory.GetDirectories(dir);

                if (directoryPaths == null || directoryPaths.Length <= 0) 
                    continue;

                foreach (string subDir in directoryPaths)
                {
                    dirs.Enqueue(subDir);
                }
            }
        }
    }
}
