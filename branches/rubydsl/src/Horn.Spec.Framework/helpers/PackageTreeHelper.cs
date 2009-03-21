using System;
using System.IO;

namespace Horn.Framework.helpers
{
    public static class PackageTreeHelper
    {
        public static void CreatePackageTreeForTesting(string hornBuildFile)
        {
            CreateDirectory(hornBuildFile);

            string builders = CreateDirectory(hornBuildFile, "builders");
            string horn = CreateDirectory(builders, "horn");

            string hornFile = Path.Combine(DirectoryHelper.GetBaseDirectory(), "horn.boo");
            string buildFile = Path.Combine(DirectoryHelper.GetBaseDirectory(), "horn.boo");

            string fileToCopy = File.Exists(hornFile) ? hornFile : buildFile;

            CopyBuildFileToDestination(fileToCopy, horn);

            string loggers = CreateDirectory(hornBuildFile, "loggers");
            string log4net = CreateDirectory(loggers, "log4net");

            var log4NetBuildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.boo");

            CopyBuildFileToDestination(log4NetBuildFile, log4net);
        }

        public static void CopyBuildFileToDestination(string sourceFile, string destinationFolder)
        {
            if (!File.Exists(sourceFile))
                throw new FileNotFoundException(string.Format("The build file {0} does not exist", sourceFile));

            string destinationBuildFile = Path.Combine(destinationFolder, "horn.boo");

            File.Copy(sourceFile, destinationBuildFile, true);
        }

        public static string CreateDirectory(string directoryPath, string newDirectoryName)
        {
            var combination = Path.Combine(directoryPath, newDirectoryName);

            return CreateDirectory(combination);
        }

        private static string CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);

            return directoryPath;
        }        
    }
}