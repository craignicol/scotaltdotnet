using Horn.Core.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_Horn_Parses_A_Successful_Configuration : BaseDSLSpecification
    {
        private BaseConfigReader configReader;

        protected override void Because()
        {
            configReader = GetConfigReaderInstance();
        }

        [Fact]
        public void Then_A_Build_Meta_Data_Object_Is_Created()
        {
            var buildMetaData = new BuildMetaData(configReader);

            AssertBuildMetaDataValues(buildMetaData);

        }
    }

    public class ConfigReaderDouble : BaseConfigReader
    {
        public override void Prepare()
        {
        }
    }
}