using Horn.Domain.BuildEngines;
using Horn.Domain.Framework;
using Horn.Domain.PackageStructure;
namespace Horn.Domain
{
    public class RakeBuildTool : IBuildTool
    {
        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }

        public string PathToBuildTool(IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }

        public string GetFrameworkVersionForBuildTool(FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }

        public void Build(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }
    }
}
