using System;
using System.IO;

namespace Horn.Framework.helpers
{
    public static class DirectoryHelper
    {
        public static string GetTempDirectoryName()
        {
            var temp = new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString()));

            InitialiseTempTreeFolder(temp);

            var packageRoot = new DirectoryInfo(Path.Combine(temp.FullName, ".horn"));

            packageRoot.Create();

            return packageRoot.FullName;
        }

        private static void InitialiseTempTreeFolder(DirectoryInfo tempTreeRootFolder)
        {
            if (!tempTreeRootFolder.Exists)
                tempTreeRootFolder.Create();


            foreach (var directory in tempTreeRootFolder.GetDirectories())
            {
                try
                {
                    directory.Delete(true);
                }
                catch
                {
                    continue;
                }
            }

            var revisionDataFile = new FileInfo(Path.Combine(tempTreeRootFolder.FullName, "revision.horn"));

            if (revisionDataFile.Exists)
                revisionDataFile.Delete();
        }

        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}