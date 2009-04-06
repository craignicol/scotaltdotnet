using System.Collections.Generic;
using Horn.Domain.BuildEngines;
using Horn.Domain.SCM;

namespace Horn.Domain.Dsl
{
    public interface IBuildMetaData
    {
        string Description { get; set; }

        Dictionary<string, object> ProjectInfo { get; set; }

        BuildEngine BuildEngine { get; set; }

        SourceControl SourceControl { get; set; }
    }
}