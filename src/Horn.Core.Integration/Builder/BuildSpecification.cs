using System.Collections.Generic;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.Utils.Framework;
using Xunit;

namespace Horn.Core.Integration.Builder
{
    public class When_The_Build_Meta_Data_Specifies_MSBuild : BuildSpecificationBase
    {
        protected override void Because()
        {
            string rootPath = GetRootPath();

            var solutionPath = Path.Combine(rootPath, "Horn.sln");

            buildEngine = new BuildEngine(new MSBuildBuildTool(), solutionPath, FrameworkVersion.frameworkVersion35);
        }

        [Fact]
        public void Then_MSBuild_Compiles_The_Source()
        {
            buildEngine.Build(packageTree);

            Assert.True(File.Exists(Path.Combine(outputPath, "Horn.Core.dll")));
        }
    }

    public class When_The_Build_Meta_Data_Specifies_Nant : BuildSpecificationBase
    {
        protected override void Because()
        {
            string rootPath = GetRootPath();

            var buildFilePath = Path.Combine(rootPath, "horn.build");

            buildEngine = new BuildEngine(new NAntBuildTool(), buildFilePath, FrameworkVersion.frameworkVersion35);
        }

        //[Fact]
        public void Then_Nant_Compiles_The_Source()
        {
            buildEngine.AssignTasks(new[] {"build"});

            buildEngine.Build(packageTree);

            Assert.True(File.Exists(Path.Combine(outputPath, "Horn.Core.dll")));            
        }
    }
}
