using System.IO;

namespace Horn.Core.PackageStructure
{
    public interface IBuildFileExtensionResolver
    {
        string Resolve(DirectoryInfo buildFolder);

        void SetFilePattern(string mask);
    }
}