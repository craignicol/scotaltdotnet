namespace Horn.Core
{
    public abstract class SourceControl
    {
        protected SourceControl(string url)
        {
            Url = url;
        }

        public virtual string Url {get; private set;}

        public abstract void Export(string destination);

        public static T Create<T>(string url)
            where T : SourceControl
        {
            T sourceControl = IoC.Resolve<T>();
            sourceControl.Url = url;

            return sourceControl;
        }
    }

    public class SVNSourceControl : SourceControl
    {
        public SVNSourceControl(string url) : base(url)
        {
        }

        public override void Export(string destination)
        {
            throw new System.NotImplementedException();
        }
    }
}