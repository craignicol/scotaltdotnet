using Horn.Core.dsl;
using Horn.Core.SCM;

namespace Horn.Core.Get
{
    using System;
    using System.IO;
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

        public virtual string Export()
        {
            string path = GetDestination(package.Name);
            fileSystemProvider.CreateDirectory(path);

            sourceControl.Export(path);

            return path;
        }

        private string GetDestination(string packageName)
        {
            string installPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Install");
            string folderName = string.Format("{0}{1}{2}", Guid.NewGuid(), Path.DirectorySeparatorChar, packageName);

            return Path.Combine(installPath, folderName);
        }

        public Get(IFileSystemProvider fileSystemProvider)
        {
            this.fileSystemProvider = fileSystemProvider;
        }
    }
}