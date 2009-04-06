
using Horn.Domain.Dsl;
using Horn.Domain.Spec.Unit.dsl;

namespace Horn.Domain.Spec
{
    public static class SpecificationHelper
    {
        public static BuildMetaData GetBuildMetaData()
        {
            var configReader = BaseDSLSpecification.GetConfigReaderInstance();

            //TODO: Fix
            return null;
            //return new BuildMetaData(configReader.);            
        }
    }
}