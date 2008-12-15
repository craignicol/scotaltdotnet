using System.Collections.Generic;
using System.IO;
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
            buildToolStub.AssertWasCalled(x => x.Build(Arg<string>.Is.Anything, Arg<List<string>>.Is.Anything, Arg<IPackageTree>.Is.Anything, Arg<FrameworkVersion>.Is.Anything));
        }
    }

    public class When_The_Build_Engine_Receives_An_Array_Of_Parameters : Specification
    {
        private string[] switches = new string[] { "sign=false", "testrunner=NUnit", "common.testrunner.enabled=true", "environment=uat", "common.testrunner.failonerror=true", "build.msbuild=true" };

        private BuildEngine buildEngine;

        protected override void Because()
        {            
            buildEngine = new BuildEngine(null, "", FrameworkVersion.frameworkVersion35);

            buildEngine.AssignParameters(switches);
        }

        [Fact]
        public void Then_A_Dictionary_Of_Switches_Is_Created()
        {
            Assert.Equal(6, buildEngine.Parameters.Keys.Count);

            Assert.Equal(6, buildEngine.Parameters.Values.Count);
        }
    }
}