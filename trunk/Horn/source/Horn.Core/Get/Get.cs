using Horn.Core.SCM;

namespace Horn.Core.GetOperations
{
    using Utils;

    public class Get : IGet
    {
        protected readonly IFileSystemProvider fileSystemProvider;
        protected Package package;
        protected SourceControl sourceControl;

        public virtual IGet Package(Package packageToGet)
        {
            package = packageToGet;
            return this;
        }

        public virtual IGet From(SourceControl sourceControlToGetFrom)
        {
            sourceControl = sourceControlToGetFrom;

            return this;
        }

        public virtual string ExportTo(string path)
        {
            sourceControl.Export(path);

            return path;
        }

        public Get(IFileSystemProvider fileSystemProvider)
        {
            this.fileSystemProvider = fileSystemProvider;
        }
    }
}