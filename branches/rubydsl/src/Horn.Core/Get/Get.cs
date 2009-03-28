using Horn.Core.PackageStructure;
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

        public virtual IPackageTree ExportTo(IPackageTree packageTree)
        {
            sourceControl.Export(packageTree);

            return packageTree;
        }

        public Get(IFileSystemProvider fileSystemProvider)
        {
            this.fileSystemProvider = fileSystemProvider;
        }
    }
}