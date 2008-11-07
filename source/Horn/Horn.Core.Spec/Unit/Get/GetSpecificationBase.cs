namespace Horn.Core.Spec.Unit.Get
{
    using Core.Get;
    using Utils;

    public abstract class GetSpecificationBase : Specification
    {
        protected IGet get;
        protected IFileSystemProvider fileSystemProvider;
        protected VersionControl versionControl;
        protected Project project;

        protected override void Before_each_spec()
        {
            versionControl = CreateStub<VersionControl>();
            project = CreateStub<Project>();

            fileSystemProvider = CreateStub<IFileSystemProvider>();
        }
    }
}