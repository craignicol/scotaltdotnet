using Horn.Domain.BuildEngines;
using Horn.Domain.Framework;
using Horn.Domain.PackageStructure;

namespace Horn.Domain
{
    public interface IBuildTool
    {
        string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version);

        string PathToBuildTool(IPackageTree packageTree, FrameworkVersion version);

        string GetFrameworkVersionForBuildTool(FrameworkVersion version);
    }
}
