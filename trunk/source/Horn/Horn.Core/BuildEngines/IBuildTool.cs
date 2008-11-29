using Horn.Core.dsl;

namespace Horn.Core
{
    public interface IBuildTool
    {
        void Build(string pathToBuildFile);
    }
}