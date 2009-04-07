using Horn.Core.Dsl;
using Horn.Domain.SCM;
using Horn.Framework.helpers;
using Rhino.DSL;
using Rhino.Mocks;
using Xunit;

namespace Horn.Domain.Spec.Unit.dsl
{
    public class When_Horn_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        private Horn.Dsl.BooConfigReader configReader;

        protected DslFactory factory;
        //private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            //dependencyResolver = CreateStub<IDependencyResolver>();
            //dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>())
            //    .Return(new SVNSourceControl(string.Empty));

           // IoC.InitializeWith(dependencyResolver);

            var engine = new ConfigReaderEngine();

            factory = new DslFactory { BaseDirectory = DirectoryHelper.GetBaseDirectory() };
            factory.Register<Horn.Dsl.BooConfigReader>(engine);
        }

        protected override void After_each_spec()
        {
            IoC.InitializeWith(null);
        }

        protected override void Because()
        {
            configReader = factory.Create<Horn.Dsl.BooConfigReader>(@"BuildConfigs/Horn/horn.boo");
            configReader.Prepare();
        }

        [Fact]
        public void Then_Horn_Returns_The_Component_DSL()
        {
            AssertHornMetaData(configReader);
        }

        //[Fact]
        //public void Should_Resolve_The_Appropriate_SourceControl()
        //{
        //    dependencyResolver.AssertWasCalled(r => r.Resolve<SVNSourceControl>());
        //}

        private void AssertHornMetaData(Horn.Dsl.BooConfigReader reader)
        {
            Assert.NotNull(reader);

            Assert.Equal("horn", reader.InstallName);

            Assert.Equal(DESCRIPTION, reader.Description);

            /*Assert.IsAssignableFrom<SVNSourceControl>(reader.SourceControl);

            Assert.Equal(SVN_URL, reader.SourceControl.Url);

            Assert.IsAssignableFrom<MSBuildBuildTool>(reader.BuildEngine.BuildTool);

            Assert.Equal(BUILD_FILE, reader.BuildEngine.BuildFile);

            Assert.Equal(1, reader.BuildEngine.Dependencies.Count);

            Assert.Equal("log4net", reader.BuildEngine.Dependencies[0].PackageName);

            Assert.Equal("lib", reader.BuildEngine.Dependencies[0].Library);*/
        }
    }
}