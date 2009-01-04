using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.dsl
{
    public class BuildMetaData : IBuildMetaData
    {
        public BuildEngine BuildEngine { get; set; }

        public SourceControl SourceControl { get; set; }

        public BuildMetaData(BaseConfigReader instance)
        {
            BuildEngine = instance.BuildEngine;

            SourceControl = instance.SourceControl;
        }
    }
}