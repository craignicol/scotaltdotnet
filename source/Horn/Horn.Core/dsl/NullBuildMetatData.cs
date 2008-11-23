using Horn.Core.SCM;

namespace Horn.Core.dsl
{
    public class NullBuildMetatData : IBuildMetaData
    {
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
    }
}