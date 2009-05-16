using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public class BuildMetaData : IBuildMetaData
    {

        public BuildEngine BuildEngine { get; set; }

        public string Description { get; set; }

        public Dictionary<string, object> ProjectInfo { get; set; }

        public SourceControl SourceControl { get; set; }

        public List<string> PrebuildCommandList { get; set; }



        public BuildMetaData()
        {
            ProjectInfo = new Dictionary<string, object>();
        }

        public BuildMetaData(BooConfigReader instance) : this()
        {
            BuildEngine = instance.BuildEngine;

            SourceControl = instance.SourceControl;
            BuildEngine.OutputDirectory = instance.BuildEngine.OutputDirectory;
            BuildEngine.SharedLibrary = instance.BuildEngine.SharedLibrary;
            ProjectInfo = instance.PackageMetaData.PackageInfo;
            PrebuildCommandList = instance.PrebuildCommandList;
        }



    }
}