using System.IO;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.BuildEngine
{
    public class When_The_Build_Engine_Is_Ran : Specification
    {
        private BuildMetaData buildMetaData;

        private IPackageTree packageTree;

        public BuildMetaData BuildMetaData
        {
            get { return buildMetaData; }
        }

        protected override void Because()
        {
            buildMetaData = SpecificationHelper.GetBuildMetaData();

            packageTree = CreateStub<IPackageTree>();

            packageTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(@"C:\"));
        }

        [Fact]
        public void Then_Build_Engine_Builds_With_The_Build_Tool()
        {
            var buildToolStub = new BuildToolStub();

            var buildEngine = new BuildEngines.BuildEngine(buildToolStub, "deeper/than/this/somebuild.file", FrameworkVersion.frameworkVersion35);

            buildEngine.Build(packageTree);

            Assert.Equal(@"C:\deeper\than\this\somebuild.file", buildToolStub.PathToBuildFile);
        }
    }












}
