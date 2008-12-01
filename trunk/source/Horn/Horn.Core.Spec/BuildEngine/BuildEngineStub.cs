using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;

namespace Horn.Core.Spec.BuildEngine
{
    public class BuildToolStub : IBuildTool
    {
        public string PathToBuildFile { get; private set; }

        public void Build(string pathToBuildFile, IPackageTree packageTree, FrameworkVersion version)
        {
            PathToBuildFile = pathToBuildFile;

            System.Console.WriteLine(pathToBuildFile);

            System.Console.WriteLine(version);
        }
    }
}