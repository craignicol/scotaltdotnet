using System;

namespace Horn.Core.dsl
{
    public class BuidMetaData
    {
        public BuidMetaData(BaseConfigReader instance)
        {
            BuildEngine = instance.BuildEngine;

            SourceControl = instance.SourceControl;
        }

        public BuildEngine BuildEngine { get; set; }

        public SourceControl SourceControl { get; set; }
    }
}