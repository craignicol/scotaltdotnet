using System;
using System.IO;

namespace Horn.Framework.helpers
{
    public static class DirectoryHelper
    {
        public static string GetTempDirectoryName()
        {
            return Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString());
        }

        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}