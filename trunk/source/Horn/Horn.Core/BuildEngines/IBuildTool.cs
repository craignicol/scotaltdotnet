using Horn.Core.Utils.Framework;

namespace Horn.Core
{
    public interface IBuildTool
    {
        void Build(string pathToBuildFile, FrameworkVersion version);
    }
}