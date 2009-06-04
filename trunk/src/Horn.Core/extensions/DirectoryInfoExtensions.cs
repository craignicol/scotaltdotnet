using System;

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
            var dirs = new Queue<string>();
            dirs.Enqueue(root.FullName);

            while (dirs.Count > 0)
            {
                string dir = dirs.Dequeue();

                string[] filePaths = null;

                //I do not like swallowing exceptions but there is a good reason
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
                        yield return file;
                    }
                }

                string[] directoryPaths = null;

                try
                {
                    directoryPaths = Directory.GetDirectories(dir);
                }
                catch
                {                    
                }
                
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
