namespace Horn.Core.Spec.Unit.PackageCommands
{
    using System.Collections.Generic;
    using Core.Dsl;
    using GetOperations;
    using Core.PackageCommands;
    using PackageStructure;
    using BuildEngine;
    using Get;
    using HornTree;
    using Utils;
    using Utils.Framework;
    using Rhino.Mocks;
    using Xunit;
    using System.IO;

    public class When_The_Builder_Receives_An_Install_Switch : Specification
    {
        protected IDictionary<string, IList<string>> switches = new Dictionary<string, IList<string>>();
        protected IGet get;
        protected IBuildConfigReader buildConfigReader;
        protected IPackageTree packageTree;
        protected IFileSystemProvider fileSystemProvider;

        protected override void Because()
        {
            switches.Add("install", new List<string>{"horn"});

            get = new Get(fileSystemProvider);

            var baseConfigReader = CreateStub<BooConfigReader>();

            baseConfigReader.InstallName = "horn";

            packageTree = CreateStub<IPackageTree>();

            var componentTree = CreateStub<IPackageTree>();

            componentTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(@"C:\")).Repeat.Any();

            packageTree.Stub(x => x.Retrieve("horn")).Return(componentTree).Repeat.Once();

            var buildFiles = new Dictionary<string, string>();

            buildFiles.Add("horn","horn");

            componentTree.BuildFiles = buildFiles;

            var buildTool = new BuildToolStub();

            var buildEngine = new BuildEngines.BuildEngine(buildTool, "Test", FrameworkVersion.FrameworkVersion35);

            baseConfigReader.BuildEngine = buildEngine;

            var buildMetaData = CreateStub<IBuildMetaData>();

            buildMetaData.SourceControl = new SourceControlDouble("svn://some.url");

            buildMetaData.BuildEngine = buildEngine;

            packageTree.Stub(x => x.Retrieve("horn")).Return(packageTree).IgnoreArguments().Repeat.Once();

            packageTree.Stub(x => x.GetBuildMetaData("horn")).Return(buildMetaData).IgnoreArguments().Repeat.Any();
            componentTree.Stub(x => x.GetBuildMetaData("log4net")).Return(buildMetaData).IgnoreArguments().Repeat.Any();
        }

        [Fact]
        public void Then_The_Builder_Coordinates_The_Build()
        {
            IPackageCommand command = new PackageBuilder(get, new StubProcessFactory());

            command.Execute(packageTree, switches);
        }
    }
}