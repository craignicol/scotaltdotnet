using Horn.Domain.Dsl;
using Horn.Domain.PackageStructure;

namespace Horn.Core.Dsl
{
    public interface IBuildConfigReader
    {
        BuildMetaData GetBuildMetaData(string packageName);

        BuildMetaData GetBuildMetaData(IPackageTree packageTree, string buildFile);

        IBuildConfigReader SetDslFactory(IPackageTree packageTree);
    }
}