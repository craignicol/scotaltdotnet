using Horn.Core.SCM;

namespace Horn.Core.Spec.Unit.Get
{
    using Core.Get;
    using Utils;

    public abstract class GetSpecificationBase : Specification
    {
        protected IGet get;
        protected IFileSystemProvider fileSystemProvider;
        protected SourceControlDouble sourceControl;
        protected Package package;

        protected override void Before_each_spec()
        {
            sourceControl = new SourceControlDouble("http://localhost/horn");

            package = new Package("horn", SpecificationHelper.GetBuildMetaData());

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