using Horn.Core.dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;

namespace Horn.Core.Spec.Unit.Get
{
    using Utils;

    public abstract class GetSpecificationBase : DirectoryStructureSpecificationBase
    {
        protected IGet get;
        protected IFileSystemProvider fileSystemProvider;
        protected SourceControlDouble sourceControl;
        protected Package package;
        protected IBuildMetaData buildMetaData;
        protected IPackageTree packageTree;

        protected override void Before_each_spec()
        {   
            base.Before_each_spec();

            sourceControl = new SourceControlDouble("http://localhost/horn");

            packageTree = new PackageTree(rootDirectory, null);

            package = new Package("horn", SpecificationHelper.GetBuildMetaData());

            fileSystemProvider = CreateStub<IFileSystemProvider>();
        }
    }

    public class SourceControlDouble : SVNSourceControl
    {
        public bool ExportWasCalled;

        protected override void Initialise(string destination)
        {
            System.Console.WriteLine("In initialise");
        }

        protected override void Download(string destination)
        {
            System.Console.WriteLine("In Download");
        }

        public override void Export(string destination)
        {
            base.Export(destination);

            ExportWasCalled = true;
        }

        public SourceControlDouble(string url)
            : base(url)
        {
        }
    }
}