using Horn.Core.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_The_Build_Config_Reader_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        protected override void Because()
        {
        }

        [Fact]
        public void Then_The_Congig_Reader_Returns_The_Ceorrect_MetaData()
        {
            IBuildConfigReader reader = new BuildConfigReaderDouble();

            BuildMetaData metaData = reader.GetBuildMetaData("horn");

            AssertBuildMetaDataValues(metaData);
        }
    }

    public class BuildConfigReaderDouble : BuildConfigReader
    {
        protected override string GetBuildFile(string sourceName)
        {
            return @"boo/projects/hornconfig.boo";
        }
    }
}