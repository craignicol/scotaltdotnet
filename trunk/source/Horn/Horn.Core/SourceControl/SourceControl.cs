namespace Horn.Core.SCM
{
    public abstract class SourceControl
    {
        public virtual string Url {get; private set;}

        public abstract void Export(string destination);

        public static T Create<T>(string url) where T : SourceControl
        {
            T sourceControl = IoC.Resolve<T>();
            sourceControl.Url = url;

            return sourceControl;
        }

        protected SourceControl(string url)
        {
            Url = url;
        }

        protected SourceControl()
        {
        }
    }
}