using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public interface IBuildTool
    {
        void Build(string pathToBuildFile, IPackageTree packageTree, FrameworkVersion version);
    }
}
