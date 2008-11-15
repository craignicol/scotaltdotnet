namespace Horn.Core.dsl
{
    public class BuildMetaData : IBuildMetaData
    {
        public BuildMetaData(BaseConfigReader instance)
        {
            BuildEngine = instance.BuildEngine;

            SourceControl = instance.SourceControl;
        }

        public BuildEngine BuildEngine { get; set; }

        public SourceControl SourceControl { get; set; }
    }
}