using System;
using Horn.Core.SCM;
using Horn.Framework.helpers;
using Rhino.DSL;

namespace Horn.Core.Spec.Unit.dsl
{
    using Core.Dsl;
    using Rhino.Mocks;
    using Xunit;

    public class When_Horn_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        private BaseConfigReader configReader;

        protected DslFactory factory;
        private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>())
                .Return(new SVNSourceControl(string.Empty));

            IoC.InitializeWith(dependencyResolver);

            var engine = new ConfigReaderEngine();

            factory = new DslFactory { BaseDirectory = DirectoryHelper.GetBaseDirectory() };
            factory.Register<BaseConfigReader>(engine);
        }

        protected override void After_each_spec()
        {
            IoC.InitializeWith(null);
        }

        protected override void Because()
        {
            configReader = factory.Create<BaseConfigReader>(@"BuildConfigs/Horn/build.boo");
            configReader.Prepare();
        }

        [Fact]
        public void Then_Horn_Returns_The_Component_DSL()
        {
            AssertHornMetaData(configReader);
        }

        [Fact]
        public void Should_Resolve_The_Appropriate_SourceControl()
        {
            dependencyResolver.AssertWasCalled(r => r.Resolve<SVNSourceControl>());
        }

        private void AssertHornMetaData(BaseConfigReader reader)
        {
            Assert.NotNull(reader);

            Assert.Equal("horn", reader.InstallName);

            Assert.Equal(DESCRIPTION, reader.Description);

            Assert.IsAssignableFrom<SVNSourceControl>(reader.SourceControl);

            Assert.Equal(SVN_URL, reader.SourceControl.Url);

            Assert.IsAssignableFrom<MSBuildBuildTool>(reader.BuildEngine.BuildTool);

            Assert.Equal(BUILD_FILE, reader.BuildEngine.BuildFile);

            Assert.Equal(1, reader.BuildEngine.Dependencies.Count);

            Assert.Equal("log4net", reader.BuildEngine.Dependencies[0].PackageName);

            Assert.Equal("lib", reader.BuildEngine.Dependencies[0].Library);
        }
    }
}