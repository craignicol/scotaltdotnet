namespace Horn.Core.extensions
{
    public static class StringExtensions
    {
        public static  string RemoveDebugFolderParts(this string part)
        {
            var ret = part.Replace("bin\\Debug\\", string.Empty).Replace("bin\\Debug", string.Empty);

            return ret;
        }
    }
}