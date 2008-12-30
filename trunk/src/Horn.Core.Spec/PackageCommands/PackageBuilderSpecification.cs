namespace Horn.Core.Spec.Unit.PackageCommands
{
    using System.Collections.Generic;
    using Core.dsl;
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

            var baseConfigReader = CreateStub<BaseConfigReader>();

            baseConfigReader.InstallName = "horn";

            packageTree = CreateStub<IPackageTree>();

            var componentTree = new PackageTreeFragnentStub();

            packageTree.Stub(x => x.Retrieve("horn")).Return(componentTree).Repeat.Once();

            var buildTool = new BuildToolStub();

            var buildEngine = new BuildEngines.BuildEngine(buildTool, "Test", FrameworkVersion.frameworkVersion35);

            baseConfigReader.BuildEngine = buildEngine;

            var buildMetaData = CreateStub<IBuildMetaData>();

            buildMetaData.SourceControl = new SourceControlDouble("svn://some.url");

            buildMetaData.BuildEngine = buildEngine;

            packageTree.Stub(x => x.Retrieve("horn")).Return(packageTree).IgnoreArguments().Repeat.Once();

            packageTree.Stub(x => x.GetBuildMetaData()).Return(buildMetaData).IgnoreArguments().Repeat.Any();
        }

        [Fact]
        public void Then_The_Builder_Coordinates_The_Build()
        {
            IPackageCommand command = new PackageBuilder(get, new StubProcessFactory());

            command.Execute(packageTree, switches);
        }
    }
}