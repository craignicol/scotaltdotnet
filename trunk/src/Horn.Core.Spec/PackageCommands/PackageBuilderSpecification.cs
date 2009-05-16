using System;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;
using Horn.Core.Spec.Doubles;
using Horn.Core.Spec.helpers;
using Horn.Core.Spec.Unit.Get;
using Horn.Framework.helpers;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    using System.Collections.Generic;
    using Core.Dependencies;
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

            wholeTree.Stub(x => x.Name).Return("horn");

            wholeTree.Stub(x => x.RetrievePackage("horn")).Return(componentTree);

            wholeTree.Stub(x => x.GetBuildMetaData("horn")).Return(buildMetaData).IgnoreArguments().Repeat.Any();

            wholeTree.Stub(x => x.OutputDirectory).Return(new DirectoryInfo(@"C:\somewhere\output"));
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

            var tempDirectory = new DirectoryInfo(DirectoryHelper.GetTempDirectoryName());

            componentTree.Stub(x => x.WorkingDirectory).Return(tempDirectory).Repeat.Any();

            componentTree.Stub(x => x.GetRevisionData()).Return(new RevisionData("3"));

            buildMetaData = GetBuildMetaData(baseConfigReader);

            componentTree.Stub(x => x.GetBuildMetaData("horn")).Return(buildMetaData);

            componentTree.Stub(x => x.Name).Return("log4net");

            componentTree.Stub(x => x.OutputDirectory).Return(new DirectoryInfo(@"C:\somewhere\output"));

            componentTree.Stub(x => x.GetBuildMetaData("log4net"))
                         .Return(buildMetaData).IgnoreArguments().Repeat.Any();

            return componentTree;
        }

        private IBuildMetaData GetBuildMetaData(BooConfigReader baseConfigReader)
        {
            var buildTool = new BuildToolStub();

            var buildEngine = new BuildEngines.BuildEngine(buildTool, "Test", FrameworkVersion.FrameworkVersion35, CreateStub<IDependencyDispatcher>());

            baseConfigReader.BuildEngine = buildEngine;

            var buildMetaData = CreateStub<IBuildMetaData>();

            buildMetaData.SourceControl = new SourceControlDoubleWithFakeFileSystem("svn://some.url");

            buildMetaData.BuildEngine = buildEngine;

            return buildMetaData;
        }
    }

    public class When_the_package_builder_receives_a_rebuild_only_switch : GetSpecificationBase
    {
        private IDependencyTree dependencyTree;

        private PackageBuilder packageBuilder;

        private MockRepository mockRepository;

        protected override void Before_each_spec()
        {
            mockRepository = new MockRepository();

            packageTree = new PackageTreeStub(TreeHelper.GetPackageTreeParts(new List<Dependency>()), "log4net", false);

            get = MockRepository.GenerateStub<IGet>();

            get.Stub(x => x.From(new SVNSourceControl("url"))).Return(get);

            get.Stub(x => x.ExportTo(packageTree)).Return(packageTree);

            dependencyTree = new DependencyTree(packageTree);

            packageBuilder = new PackageBuilder(get, MockRepository.GenerateStub<IProcessFactory>());
        }

        protected override void Because()
        {
            var args = new Dictionary<string, IList<string>>();

            args.Add("install", new List<string>{"log4net"});
            args.Add("rebuildonly", new List<string>{""});

            mockRepository.Playback();
            
            packageBuilder.Execute(packageTree, args);
        }

        [Fact]
        public void Then_source_control_get_is_not_called()
        {
            get.AssertWasNotCalled(x => x.From(Arg<SVNSourceControl>.Is.TypeOf));
        }
    }
}