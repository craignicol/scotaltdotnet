using System;
using System.Collections.Generic;
using Horn.Core.BuildEngines;
using Horn.Core.SCM;

namespace Horn.Core.Dsl
{
    public class NullBuildMetatData : IBuildMetaData
    {
        public string Description
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public List<SourceControl> ExportList
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public List<RepositoryInclude> IncludeList
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string InstallName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Dictionary<string, object> ProjectInfo
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public BuildEngine BuildEngine
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public SourceControl SourceControl
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public List<string> PrebuildCommandList
        {
            get; set;
        }

        public string OutputDirectory
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}