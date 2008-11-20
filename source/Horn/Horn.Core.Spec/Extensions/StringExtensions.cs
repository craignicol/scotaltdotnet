namespace Horn.Core.Spec.Extensions
{
    public static class StringExtensions
    {
        public static string ResolvePath(this string root)
        {
            return (root.IndexOf("3.5") == -1) ? root.Replace("bin\\debug", "") : root + "\\";
        }
    }
}