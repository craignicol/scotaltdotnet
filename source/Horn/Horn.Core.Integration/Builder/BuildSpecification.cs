using System;
using System.IO;
using Horn.Core.PackageStructure;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Integration.Builder
{
    public class When_The_Build_Meta_Data_Specifies_MSBuild : TestBase
    {
        private BuildEngine buildEngine;
        private IPackageTree packageTree;
        private string outputPath;
        

        protected override void Because()
        {
            outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");

            if(Directory.Exists(outputPath))
                Directory.Delete(outputPath);

            Directory.CreateDirectory(outputPath);

            packageTree = MockRepository.GenerateStub<IPackageTree>();

            packageTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(@"C:\Projects\horn\trunk\source\Horn\"));

            packageTree.Stub(x => x.OutputDirectory).Return(new DirectoryInfo(outputPath));

            buildEngine = new BuildEngine(new MSBuildBuildTool(), @"C:\Projects\horn\trunk\source\Horn\Horn.sln");
        }

        [Fact]
        public void The_Rutime_Version_Of_MSBuild_Is_Selected()
        {
            Assert.True(File.Exists(MSBuildBuildTool.MSBuildPath));
        }


        [Fact]
        public void Then_MSBuild_Compiles_The_Source()
        {
            buildEngine.Build(packageTree);

            Assert.True(File.Exists(Path.Combine(outputPath, "Horn.Core.dll")));
        }
    }
}