using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.dsl
{
    public interface IBuildMetaData
    {
        BuildEngine BuildEngine { get; set; }
        SourceControl SourceControl { get; set; }
    }
}