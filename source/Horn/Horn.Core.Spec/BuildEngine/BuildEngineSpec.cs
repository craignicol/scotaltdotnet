using System.IO;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.BuildEngine
{
    public class When_Nant_Is_Specified_And_the_Source_Control_Has_Been_downloaded_To_The_Sandbox : Specification
    {
        private BuildMetaData buildMetaData;

        private IPackageTree packageTree;

        protected override void Because()
        {
            buildMetaData = SpecificationHelper.GetBuildMetaData();

            packageTree = CreateStub<IPackageTree>();

            packageTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(@"C:\"));
        }

        [Fact]
        public void Then_Horn_Compiles_The_Source()
        {
            BuildToolStub buildToolStub = new BuildToolStub();

            var buildEngine = new Core.BuildEngine(buildToolStub, "somebuild.file", FrameworkVersion.frameworkVersion35);

            buildEngine.Build(packageTree);

            Assert.Equal(@"C:\somebuild.file", buildToolStub.PathToBuildFile);
        }
    }

    public class BuildToolStub : IBuildTool
    {
        public string PathToBuildFile { get; private set; }

        public void Build(string pathToBuildFile, FrameworkVersion version)
        {
            PathToBuildFile = pathToBuildFile;

            System.Console.WriteLine(pathToBuildFile);
        }
    }
}