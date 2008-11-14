using System.IO;
using Horn.Core.dsl;
using Rhino.Mocks;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.Unit.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.HornTree
{
    public class When_Given_The_Package_Root_Directory : PackageTreeSpecificationBase
    {
        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);
        }

        [Fact]
        public void Then_Horn_Builds_An_In_Memory_Tree_Structure_Of_The_Builds()
        {
            IPackageTree hornTree = new PackageTree(rootDirectory, null);

            AssertPackageTreeStructure(hornTree);
        }

        private void AssertPackageTreeStructure(IPackageTree hornTree)
        {
            Assert.True(hornTree.IsRoot);

            Assert.Equal(1, hornTree.Children.Count);

            Assert.Equal(1, hornTree.Children[0].Children.Count);

            Assert.Equal(rootDirectory.FullName, hornTree.CurrentDirectory.FullName);
        }
    }

    public class When_Given_A_Request_For_A_Build_File : PackageTreeSpecificationBase
    {
        private IPackageTree hornTree;

        private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            base.Before_each_spec();

            IBuildConfigReader buildConfigReader = new BuildConfigReader();

            dependencyResolver = CreateStub<IDependencyResolver>();

            dependencyResolver.Stub(x => x.Resolve<IBuildConfigReader>()).Return(buildConfigReader);

            var svn = new SVNSourceControl("https://svnserver/trunk");

            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(svn);

            IoC.InitializeWith(dependencyResolver);
        }

        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);

            hornTree = new PackageTree(rootDirectory, null);            
        }

        [Fact]
        public void Then_Horn_Retrieves_The_Build_File_From_The_Structure()
        {
            var metaData = hornTree.Retrieve("horn").GetBuildMetaData();

            BaseDSLSpecification.AssertBuildMetaDataValues(metaData);
        }
    }
}