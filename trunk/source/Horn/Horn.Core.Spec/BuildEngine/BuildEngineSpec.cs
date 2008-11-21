using Horn.Core.dsl;
using Horn.Core.Spec.Unit.dsl;
using Rhino.Mocks;
using Xunit;

namespace Horn.Core.Spec.BuildEngine
{
    public class When_Nant_Is_Specified_And_the_Source_Control_Has_Been_downloaded_To_The_Sandbox : Specification
    {
        //private BuildMetaData buildMetaData;

        protected override void Because()
        {
            var buildConfigReader = CreateStub<BuildConfigReader>();

            var configReader = BaseDSLSpecification.GetConfigReaderInstance();

            
        }

        [Fact]
        public void Then_Horn_Compiles_The_Source()
        {
        }
    }
}