namespace Horn.Core.Spec.Extensions
{
    public static class StringExtensions
    {
        public static string ResolvePath(this string root)
        {
            if(root.IndexOf("3.5") == 1)
                return root.Replace("bin\\debug", "").Replace("build\\net-3.5\\debug", "");

            return root + "\\";
        }
    }
}