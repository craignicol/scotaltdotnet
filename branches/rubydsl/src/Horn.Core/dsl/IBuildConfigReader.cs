using System.IO;

namespace Horn.Core.Dsl
{
    public interface IBuildConfigReader
    {
        BuildMetaData GetBuildMetaData(string fileName);

        IBuildConfigReader SetDslFactory(DirectoryInfo rootDirectory);
    }
}