using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public interface IBuildMetaData
    {
        BuildEngine BuildEngine { get; set; }
            
        string Description { get; set; }

        List<SourceControl> ExportList { get; set; }

        List<RepositoryInclude> IncludeList { get; set; }

        string InstallName { get; set; }

        List<string> PrebuildCommandList { get; set; }

        Dictionary<string, object> ProjectInfo { get; set; }

        SourceControl SourceControl { get; set; }
    }
}