using System.Collections.Generic;
using Horn.Domain.BuildEngines;
using Horn.Domain.SCM;

namespace Horn.Domain.Dsl
{
    public class NullBuildMetatData : IBuildMetaData
    {
        public string Description
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
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

        public string OutputDirectory
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}