using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
public class BuildMetaData : IBuildMetaData
{

    public BuildEngine BuildEngine { get; set; }

    public string Description { get; set; }

    public SourceControl SourceControl { get; set; }

    public BuildMetaData(){}

    public BuildMetaData(BaseConfigReader instance)
    {
        BuildEngine = instance.BuildEngine;

        SourceControl = instance.SourceControl;
    }
}
}