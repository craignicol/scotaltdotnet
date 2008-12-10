using System.IO;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.BuildEngine
{
    using BuildEngines;

    public class When_The_Build_Engine_Is_Ran : Specification
    {
        private IPackageTree packageTree;
        private IBuildTool buildToolStub;
        private BuildEngine buildEngine;

        protected override void Because()
        {
            packageTree = CreateStub<IPackageTree>();

            packageTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(@"C:\"));
            
            buildToolStub = CreateStub<IBuildTool>();

            buildEngine = new BuildEngine(buildToolStub, "deeper/than/this/somebuild.file", FrameworkVersion.frameworkVersion35);
            
            buildEngine.Build(packageTree);
        }

        [Fact]
        public void Then_Build_Engine_Builds_With_The_Build_Tool()
        {
            buildToolStub.AssertWasCalled(x => x.Build(Arg<string>.Is.Anything, Arg<IPackageTree>.Is.Anything, Arg<FrameworkVersion>.Is.Anything));
        }
    }












}
