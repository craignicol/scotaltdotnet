using System;
using System.IO;

namespace Horn.Framework.helpers
{
    public static class DirectoryHelper
    {
        public static string GetTempDirectoryName()
        {
            var tempPackageTreePath = new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable("temp"), "temppackagetrees"));

            InitialiseTempTreeFolder(tempPackageTreePath);

            var ret = new DirectoryInfo(Path.Combine(tempPackageTreePath.FullName, Guid.NewGuid().ToString()));

            ret.Create();

            return ret.FullName;
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
        }

        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}