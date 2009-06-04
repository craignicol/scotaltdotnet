using System.IO;

namespace Horn.Core.extensions
{
    public static class StringExtensions
    {

        public static  string RemoveDebugFolderParts(this string part)
        {
            var ret = part.Replace("bin\\x86\\Debug\\", string.Empty).Replace("bin\\Debug", string.Empty);

            return ret;
        }

        public static bool PathIsFile(this string fullPath)
        {
            return (Path.GetExtension(fullPath).Length > 0);
        }



    }
}