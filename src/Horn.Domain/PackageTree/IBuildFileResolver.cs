using System.IO;

namespace Horn.Domain.PackageStructure
{
    public interface IBuildFileResolver
    {
        BuildFileResolver Resolve(DirectoryInfo buildFolder, string fileName);

        string BuildFile { get; }

        string Extension { get; }
    }
}