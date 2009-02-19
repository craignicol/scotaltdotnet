using Horn.Core.dsl;
using Horn.Core.Spec.Unit.dsl;

namespace Horn.Core.Spec
{
    public static class SpecificationHelper
    {
        public static BuildMetaData GetBuildMetaData()
        {
            var configReader = BaseDSLSpecification.GetConfigReaderInstance();

            return new BuildMetaData(configReader);            
        }
    }
}