using Horn.Core;
using Horn.Core.GetOperations;
using Horn.Core.Utils;
using Horn.Domain.Dsl;
using Horn.Domain.PackageStructure;

namespace Horn.Domain.Spec.Unit.Get
{
    public abstract class GetSpecificationBase : DirectorySpecificationBase
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
}