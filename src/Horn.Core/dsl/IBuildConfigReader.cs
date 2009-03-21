using System.IO;
using Horn.Core.PackageStructure;

namespace Horn.Core.Dsl
{
    public interface IBuildConfigReader
    {
        BuildMetaData GetBuildMetaData(string packageName);

        BuildMetaData GetBuildMetaData(DirectoryInfo buildFolder, string buildFile);

        IBuildConfigReader SetDslFactory(DirectoryInfo rootDirectory);
    }
}