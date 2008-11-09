namespace Horn.Core.Get
{
    using System;
    using System.IO;
    using Utils;

    public class Get : IGet
    {
        protected readonly IFileSystemProvider fileSystemProvider;
        protected Project project;
        protected SourceControl sourceControl;

        public Get(IFileSystemProvider fileSystemProvider)
        {
            this.fileSystemProvider = fileSystemProvider;
        }

        public virtual IGet Project(Project projectToGet)
        {
            project = projectToGet;
            return this;
        }

        public virtual IGet From(SourceControl sourceControlToGetFrom)
        {
            sourceControl = sourceControlToGetFrom;
            return this;
        }

        public virtual string Export()
        {
            string basePath = GetDestination(project.Name);
            string path = fileSystemProvider.CreateDirectory(basePath);

            sourceControl.Export(path);

            return path;
        }

        private string GetDestination(string projectName)
        {
            string installPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Install");
            string folderName = string.Format("{0}{1}{2}", Guid.NewGuid(), Path.DirectorySeparatorChar, projectName);

            return Path.Combine(installPath, folderName);
        }
    }
}