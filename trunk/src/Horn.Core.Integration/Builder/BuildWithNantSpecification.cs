using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.Utils.Framework;
using Xunit;

namespace Horn.Core.Integration.Builder
{
    public class When_The_Build_MetaData_Specifies_MSBuild : BuildSpecificationBase
    {
        protected override void Because()
        {
            var rootPath = GetRootPath();

            var path = Path.Combine("horn.build", rootPath);

            buildEngine = new BuildEngine(new NAntBuildTool(), path, FrameworkVersion.frameworkVersion35);
        }

        [Fact]
        public void Then_Nant_Builds_The_Source()
        {
            buildEngine.Build(packageTree);

            Assert.True(File.Exists(Path.Combine(outputPath, "Horn.Core.dll")));
        }
    }
}