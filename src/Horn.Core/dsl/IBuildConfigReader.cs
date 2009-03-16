using System.IO;

namespace Horn.Core.Dsl
{
    public interface IBuildConfigReader
    {
        BuildMetaData GetBuildMetaData();

        IBuildConfigReader SetDslFactory(DirectoryInfo rootDirectory);
    }
}