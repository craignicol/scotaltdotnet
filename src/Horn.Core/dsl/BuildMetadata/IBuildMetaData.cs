using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public interface IBuildMetaData
    {
        string Description { get; set; }

        Dictionary<string, object> ProjectInfo { get; set; }

        BuildEngine BuildEngine { get; set; }

        SourceControl SourceControl { get; set; }
    }
}