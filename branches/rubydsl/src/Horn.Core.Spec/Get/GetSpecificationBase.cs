using System.IO;
using Horn.Core.Dsl;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Horn.Framework.helpers;

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

            packageTree = new PackageTree(rootDirectory, null, new BuildFileExtensionResolver());

            package = new Package("horn", SpecificationHelper.GetBuildMetaData());

            fileSystemProvider = CreateStub<IFileSystemProvider>();
        }
    }
}