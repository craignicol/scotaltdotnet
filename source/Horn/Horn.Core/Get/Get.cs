namespace Horn.Core.Get
{
    using Utils;

    public class Get : IGet
    {
        protected readonly IFileSystemProvider fileSystemProvider;
        protected Project project;
        protected VersionControl versionControl;

        public Get(IFileSystemProvider fileSystemProvider)
        {
            this.fileSystemProvider = fileSystemProvider;
        }

        public virtual IGet Project(Project projectToGet)
        {
            project = projectToGet;
            return this;
        }

        public virtual IGet From(VersionControl versionControlToGetFrom)
        {
            versionControl = versionControlToGetFrom;
            return this;
        }

        public virtual void Export()
        {
            fileSystemProvider.CreateDirectory(project.SourcePath);
            versionControl.Export(project.GetVersionControlParameters());
        }
    }
}