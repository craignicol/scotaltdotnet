using System;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using Horn.Spec.Framework.Extensions;
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
            outputPath = CreateDirectory("Output");

            var working = CreateDirectory("Working");

            packageTree = MockRepository.GenerateStub<IPackageTree>();

            packageTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(working));


            packageTree.Stub(x => x.OutputDirectory).Return(new DirectoryInfo(outputPath));

            string rootPath =
                new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.ResolvePath()).Parent.FullName;

            var solutionPath = Path.Combine(rootPath, "Horn.sln");

            buildEngine = new BuildEngine(new MSBuildBuildTool(), solutionPath, FrameworkVersion.frameworkVersion35);
        }

        [Fact]
        public void Then_MSBuild_Compiles_The_Source()
        {
            buildEngine.Build(packageTree);

            Assert.True(File.Exists(Path.Combine(outputPath, "Horn.Core.dll")));
        }


        private string CreateDirectory(string directoryName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryName);

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);

            return path;
        }
    }
}
