using System.IO;

namespace Horn.Core.dsl
{
    public interface IBuildConfigReader
    {
        BuildMetaData GetBuildMetaData();

        IBuildConfigReader SetDslFactory(DirectoryInfo rootDirectory);
    }
}