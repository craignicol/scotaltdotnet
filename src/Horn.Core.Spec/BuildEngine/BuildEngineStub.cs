namespace Horn.Core.Spec.BuildEngine
{
    using System.Collections.Generic;
    using PackageStructure;
    using Utils.Framework;
    using BuildEngines;

    public class BuildToolStub : IBuildTool
    {
        public string PathToBuildFile { get; private set; }

        public string CommandLineArguments(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            throw new System.NotImplementedException();
        }

        public void Build(string pathToBuildFile, BuildEngine buildEngine, IPackageTree packageTree, FrameworkVersion version)
        {
            PathToBuildFile = pathToBuildFile;

            System.Console.WriteLine(pathToBuildFile);

            System.Console.WriteLine(version);
        }
    }

}