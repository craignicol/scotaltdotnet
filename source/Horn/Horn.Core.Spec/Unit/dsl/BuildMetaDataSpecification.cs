using Horn.Core.dsl;
using Rhino.DSL;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_Horn_Parses_A_Successful_Configuration : BaseDSLSpecification
    {
        private BaseConfigReader instance;

        //private DslFactory factory;

        protected override void Because()
        {
            //factory = MockRepository.GenerateStub<DslFactory>();

            instance = new ConfigReaderDouble();

            instance.description(DESCRIPTION);
            instance.BuildEngine = new RakeBuildEngine(BUILD_FILE);
            instance.SourceControl = new SVNSourceControl(SVN_URL);

            instance.BuildEngine.AssignTasks(TASKS.ToArray());

            //factory.Stub(x => x.Create<BaseConfigReader>(@"boo/projects/hornconfig.boo")).Return(instance);
        }


        [Fact]
        public void Then_A_Build_Meta_Data_Object_Is_Created()
        {
            //instance = factory.Create<BaseConfigReader>(@"boo/projects/hornconfig.boo");

            var buildMetaData = new BuidMetaData(instance);

            AssertBuildMetaDataValues(buildMetaData);

        }

        private void AssertBuildMetaDataValues(BuidMetaData metaData)
        {
            Assert.IsAssignableFrom(typeof(SVNSourceControl), metaData.SourceControl);

            Assert.Equal(SVN_URL, metaData.SourceControl.Url);

            Assert.IsAssignableFrom(typeof (RakeBuildEngine), metaData.BuildEngine);

            Assert.Equal(BUILD_FILE, metaData.BuildEngine.BuildFile);

            Assert.Equal(3, metaData.BuildEngine.Tasks.Count);
        }
    }

    public class ConfigReaderDouble : BaseConfigReader
    {
        public override void Prepare()
        {
        }
    }
}