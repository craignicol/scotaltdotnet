using System;
using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public class BuildMetaData : IBuildMetaData
    {

        public BuildEngine BuildEngine { get; set; }

        public string Description { get; set; }

        public List<SourceControl> ExportList{ get; set; }

        public List<RepositoryElement> RepositoryElementList { get; set; }

        public string InstallName { get; set; }

        public List<string> PrebuildCommandList { get; set; }

        public Dictionary<string, object> ProjectInfo { get; set; }

        public SourceControl SourceControl { get; set; }



        public BuildMetaData()
        {
            ProjectInfo = new Dictionary<string, object>();

            ExportList = new List<SourceControl>();

            RepositoryElementList = new List<RepositoryElement>();

            PrebuildCommandList = new List<string>();
        }



    }
}