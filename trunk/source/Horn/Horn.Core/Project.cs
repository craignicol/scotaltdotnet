namespace Horn.Core
{
    using Get;

    /// <summary>
    /// This class is possibly a temp class.  I added it to facilate passing around information that would be pulled out from the
    /// MetaData.
    /// </summary>
    public class Project
    {
        public virtual VersionControlParameters GetVersionControlParameters()
        {
            return new VersionControlParameters();
        }

        public virtual string SourcePath { get; private set; }
    }
}