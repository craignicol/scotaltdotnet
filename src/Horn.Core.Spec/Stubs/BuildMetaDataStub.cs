using System;
using System.Collections.Generic;
using Horn.Core.Dsl;

namespace Horn.Core.Spec.Doubles
{
    public class BuildMetaDataStub : IBuildMetaData
    {
        public string Description { get; set; }
        public List<SCM.SourceControl> ExportList { get; set; }

        public Dictionary<string, object> ProjectInfo { get; set; }

        public BuildEngines.BuildEngine BuildEngine { get; set; }

        public SCM.SourceControl SourceControl { get; set; }

        public List<string> PrebuildCommandList{get; set;}

        public BuildMetaDataStub(BuildEngines.BuildEngine buildEngine, SCM.SourceControl sourceControl)
        {
            BuildEngine = buildEngine;
            SourceControl = sourceControl;
            PrebuildCommandList = new List<string>();

            ExportList = new List<SCM.SourceControl>();
        }
    }
}