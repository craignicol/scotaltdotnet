
using Horn.Domain.Dsl;
using Xunit;

namespace Horn.Domain.Spec.Unit.dsl
{
    public class When_Horn_Parses_A_Successful_Configuration : BaseDSLSpecification
    {
        private BooConfigReader configReader;

        protected override void Because()
        {
            configReader = GetConfigReaderInstance();
        }

        [Fact]
        public void Then_A_Build_Meta_Data_Object_Is_Created()
        {
            //TODO: Fix
            var buildMetaData = new BuildMetaData(null, null);

            AssertBuildMetaDataValues(buildMetaData);

        }
    }

    public class ConfigReaderDouble : BooConfigReader
    {
        public override void Prepare()
        {
        }
    }
}