using System;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Framework.helpers;
using Rhino.DSL;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_retrieving_from_a_repository : Specification
    {
        private BooConfigReader configReader;

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
            factory.Register<BooConfigReader>(engine);
        }

        protected override void After_each_spec()
        {
            IoC.InitializeWith(null);
        }

        protected override void Because()
        {
            configReader = factory.Create<BooConfigReader>(@"BuildConfigs/Horn/repository.boo");
            configReader.Prepare();
        }

        [Fact]
        public void Then_the_model_contains_the_repository_details()
        {
            Assert.Equal("castle", configReader.BuildMetaData.IncludeList[0].RepositoryName);
            Assert.Equal("here", configReader.BuildMetaData.IncludeList[0].IncludePath);
            Assert.Equal("there", configReader.BuildMetaData.IncludeList[0].ExportPath);
            Assert.Equal("castle", configReader.BuildMetaData.IncludeList[1].RepositoryName);
            Assert.Equal("over", configReader.BuildMetaData.IncludeList[1].IncludePath);
            Assert.Equal("out", configReader.BuildMetaData.IncludeList[1].ExportPath);
        }
    }

    public class When_we_need_an_include_list : Specification
    {
        private const string RepositoryName = "repository";

        private const string IncludePath = "here";

        private const string ExportPath = "there";

        private RepositoryInclude repositoryInclude;

        protected override void Because()
        {
            repositoryInclude = new RepositoryInclude(RepositoryName, IncludePath, ExportPath);
        }

        [Fact]
        public void Then_the_model_can_express_this()
        {
            Assert.Equal(RepositoryName, repositoryInclude.RepositoryName);
            Assert.Equal(IncludePath, repositoryInclude.IncludePath);
            Assert.Equal(ExportPath, repositoryInclude.ExportPath);
        }
    }
}