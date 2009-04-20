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

                // files
                string[] paths = null;
                try
                {
                    paths = Directory.GetFiles(dir, searchPattern);
                }
                catch { } // swallow

                if (paths != null && paths.Length > 0)
                {
                    foreach (string file in paths)
                    {
                        yield return file;
                    }
                }

                // sub-directories
                paths = null;
                try
                {
                    paths = Directory.GetDirectories(dir);
                }
                catch { } // swallow

                if (paths != null && paths.Length > 0)
                {
                    foreach (string subDir in paths)
                    {
                        dirs.Enqueue(subDir);
                    }
                }
            }
        }
    }
}
