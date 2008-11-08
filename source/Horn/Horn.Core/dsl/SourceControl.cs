namespace Horn.Core.dsl
{
    public abstract class SourceControl
    {
        protected SourceControl(string url)
        {
            Url = url;
        }

        public virtual string Url {get; private set;}
    }

    public class SVNSourceControl : SourceControl
    {
        public SVNSourceControl(string url) : base(url)
        {
        }
    }
}