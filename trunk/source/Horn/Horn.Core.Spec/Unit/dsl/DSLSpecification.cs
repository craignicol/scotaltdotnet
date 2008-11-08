using System;
using Rhino.DSL;

namespace Horn.Core.Spec.Unit.dsl
{
    using Core.dsl;
    using Xunit;

    public class When_Horn_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        private BaseConfigReader configReader;

        protected DslFactory factory;

        protected override void Before_each_spec()
        {
            factory = new DslFactory { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory };
            factory.Register<BaseConfigReader>(new ConfigReaderEngine());
        }

        protected override void Because()
        {
            configReader = factory.Create<BaseConfigReader>(@"boo/projects/hornconfig.boo");
            configReader.Prepare();
        }

        [Fact]
        public void Then_Horn_Returns_The_Component_DSL()
        {
            AssertHornMetaData(configReader);
        }

        private void AssertHornMetaData(BaseConfigReader reader)
        {
            Assert.NotNull(reader);

            Assert.Equal("horn", reader.InstallName);

            Assert.Equal(DESCRIPTION, reader.Description);

            Assert.IsAssignableFrom(typeof (SVNSourceControl), reader.SourceControl);

            Assert.Equal(SVN_URL, reader.SourceControl.Url);

            Assert.IsAssignableFrom(typeof (RakeBuildEngine), reader.BuildEngine);

            Assert.Equal(BUILD_FILE, reader.BuildEngine.BuildFile);

            Assert.Equal(reader.BuildEngine.Tasks[0], TASKS[0]);

            Assert.Equal(reader.BuildEngine.Tasks[1], TASKS[1]);

            Assert.Equal(reader.BuildEngine.Tasks[2], TASKS[2]);
        }
    }
}