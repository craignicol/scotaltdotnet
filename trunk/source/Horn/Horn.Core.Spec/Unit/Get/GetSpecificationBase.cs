namespace Horn.Core.Spec.Unit.Get
{
    using Core.Get;
    using Utils;

    public abstract class GetSpecificationBase : Specification
    {
        protected IGet get;
        protected IFileSystemProvider fileSystemProvider;
        protected SourceControlDouble sourceControl;
        protected Project project;

        protected override void Before_each_spec()
        {
            sourceControl = new SourceControlDouble("http://localhost/horn");
            project = new Project {Name = "horn"};

            fileSystemProvider = CreateStub<IFileSystemProvider>();
        }
    }

    public class SourceControlDouble : SourceControl
    {
        public bool ExportWasCalled;

        public SourceControlDouble(string url) : base(url)
        {
        }

        public override void Export(string destination)
        {
            ExportWasCalled = true;
        }
    }
}