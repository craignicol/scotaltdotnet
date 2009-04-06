using System.Collections.Generic;
using Horn.Domain.BuildEngines;
using Horn.Domain.SCM;

namespace Horn.Domain.Dsl
{
    public class BuildMetaData : IBuildMetaData
    {

        public BuildEngine BuildEngine { get; set; }

        public string Description { get; set; }

        public Dictionary<string, object> ProjectInfo { get; set; }

        public SourceControl SourceControl { get; set; }



        public BuildMetaData()
        {
            ProjectInfo = new Dictionary<string, object>();
        }

        public BuildMetaData(BuildEngine buildEngine, SourceControl sourceControl) : this()
        {
            BuildEngine = buildEngine;

            SourceControl = sourceControl;
            BuildEngine.OutputDirectory = buildEngine.OutputDirectory;
            BuildEngine.SharedLibrary = buildEngine.SharedLibrary;
        }



    }
}