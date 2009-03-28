using System;
using System.IO;

namespace Horn.Framework.helpers
{
    public static class PackageTreeHelper
    {
        public const string PACKAGE_WITHOUT_REVISION = "norevisionpackage";
        public  const string PACKAGE_WITH_REVISION = "log4net";

        public static DirectoryInfo CreateEmptyDirectoryStructureForTesting()
        {
            var rootDirectory = DirectoryHelper.GetTempDirectoryName();

            CreateDirectory(rootDirectory);

            return new DirectoryInfo(rootDirectory);
        }

        public static DirectoryInfo CreateDirectoryStructureForTesting()
        {
            var rootDirectory = DirectoryHelper.GetTempDirectoryName();

            CreateDirectory(rootDirectory);

            string builders = CreateDirectory(rootDirectory, "builders");
            string horn = CreateDirectory(builders, "horn");

            string hornFile = Path.Combine(DirectoryHelper.GetBaseDirectory(), "horn.boo");
            string buildFile = Path.Combine(DirectoryHelper.GetBaseDirectory(), "horn.boo");

            string fileToCopy = File.Exists(hornFile) ? hornFile : buildFile;

            CreateBuildFiles(fileToCopy, horn, true);

            string loggers = CreateDirectory(rootDirectory, "loggers");
            string log4net = CreateDirectory(loggers, "log4net");

            var log4NetBuildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.boo");

            CreateBuildFiles(log4NetBuildFile, log4net, true);

            string tests = CreateDirectory(rootDirectory, "tests");
            string norevisionpackage = CreateDirectory(tests, PACKAGE_WITHOUT_REVISION);

            CreateBuildFiles(log4NetBuildFile, norevisionpackage, false);

            return new DirectoryInfo(rootDirectory);
        }

        public static void CreateBuildFiles(string sourceFile, string destinationFolder, bool createRevisionFile)
        {
            if (!File.Exists(sourceFile))
                throw new FileNotFoundException(string.Format("The build file {0} does not exist", sourceFile));

            string destinationBuildFile = Path.Combine(destinationFolder, Path.GetFileName(sourceFile));

            File.Copy(sourceFile, destinationBuildFile, true);

            if (!createRevisionFile)
                return;

            var revisionFile = Path.Combine(destinationFolder, "revision.horn");

            using(var streamWriter = new StreamWriter(revisionFile))
            {
                streamWriter.Write("revision=1");

                streamWriter.Close();
            }
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