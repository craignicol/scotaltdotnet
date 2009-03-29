namespace Horn.Core.Spec.Unit.PackageCommands
{
    using System.Collections.Generic;
    using Core.Dsl;
    using GetOperations;
    using Core.PackageCommands;
    using PackageStructure;
    using BuildEngine;
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
        protected IPackageTree wholeTree;
        protected IFileSystemProvider fileSystemProvider;

        protected override void Because()
        {
            get = new Get(fileSystemProvider);

            IBuildMetaData buildMetaData;

            IPackageTree componentTree = GetComponentTree(out buildMetaData);

            wholeTree = CreateStub<IPackageTree>();

            wholeTree.Stub(x => x.RetrievePackage("horn")).Return(componentTree);

            wholeTree.Stub(x => x.GetBuildMetaData("horn")).Return(buildMetaData).IgnoreArguments().Repeat.Any();
        }

        [Fact]
        public void Then_The_Builder_Coordinates_The_Build()
        {
            switches.Add("install", new List<string> { "horn" });

            IPackageCommand command = new PackageBuilder(get, new StubProcessFactory());

            command.Execute(wholeTree, switches);
        }

        private IPackageTree GetComponentTree(out IBuildMetaData buildMetaData)
        {
            var baseConfigReader = CreateStub<BooConfigReader>();

            baseConfigReader.InstallName = "horn";

            var componentTree = CreateStub<IPackageTree>();

            componentTree.Stub(x => x.WorkingDirectory).Return(new DirectoryInfo(@"c:\temp\safe")).Repeat.Any();

            componentTree.Stub(x => x.GetRevisionData()).Return(new RevisionData("3"));

            buildMetaData = GetBuildMetaData(baseConfigReader);

            componentTree.Stub(x => x.GetBuildMetaData("horn")).Return(buildMetaData);

            componentTree.Stub(x => x.GetBuildMetaData("log4net"))
                         .Return(buildMetaData).IgnoreArguments().Repeat.Any();

            return componentTree;
        }

        private IBuildMetaData GetBuildMetaData(BooConfigReader baseConfigReader)
        {
            var buildTool = new BuildToolStub();

            var buildEngine = new BuildEngines.BuildEngine(buildTool, "Test", FrameworkVersion.FrameworkVersion35);

            baseConfigReader.BuildEngine = buildEngine;

            var buildMetaData = CreateStub<IBuildMetaData>();

            buildMetaData.SourceControl = new SourceControlDoubleWithFakeFileSystem("svn://some.url");

            buildMetaData.BuildEngine = buildEngine;

            return buildMetaData;
        }
    }
}