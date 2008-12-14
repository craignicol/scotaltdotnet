using System;
using Horn.Core.dsl;
using Horn.Core.SCM;
using Rhino.DSL;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_Nant_Is_Specified_In_The_Dsl_As_The_Build_Tool : Specification
    {
        private BaseConfigReader configReader;

        protected DslFactory factory;
        private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(new SVNSourceControl(string.Empty));

            IoC.InitializeWith(dependencyResolver);

            factory = new DslFactory { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory };
            factory.Register<BaseConfigReader>(new ConfigReaderEngine());
        }

        protected override void Because()
        {
            configReader = factory.Create<BaseConfigReader>(@"BuildConfigs/Horn/buildnant.boo");
            configReader.Prepare(); 
        }

        [Fact]
        public void Then_The_Dsl_Compiles()
        {
            Assert.IsAssignableFrom<NAntBuildTool>(configReader.BuildEngine.BuildTool);

            Assert.Equal(1, configReader.BuildEngine.Tasks.Count);

            Assert.Equal("build", configReader.BuildEngine.Tasks[0]);
        }
    }
}