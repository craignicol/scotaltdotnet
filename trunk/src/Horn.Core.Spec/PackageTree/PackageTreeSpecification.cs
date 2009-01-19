using System.IO;
using Horn.Core.dsl;
using Horn.Core.SCM;
using Rhino.Mocks;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.Unit.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.HornTree
{
    public class When_Given_The_Package_Root_Directory : PackageTreeSpecificationBase
    {
        private IPackageTree rootTree;

        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);
            rootTree = new PackageTree(rootDirectory, null);
        }

        [Fact]
        public void Is_the_root()
        {
            Assert.True(rootTree.IsRoot);
        }

        [Fact]
        public void Will_Have_A_Child()
        {
            Assert.Equal(2, rootTree.Children.Length);
        }

        [Fact]
        public void CurrentDirector_Will_Be_the_Root_Directory()
        {
            Assert.Equal(rootDirectory.FullName, rootTree.CurrentDirectory.FullName); 
        }
    }

    public class When_CreateDefaultTreeStructure_Is_Executed_On_A_Clean_Install : PackageTreeSpecificationBase
    {
        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);
        }

        [Fact]
        public void Check_That_There_Is_No_Existing_PackageTree()
        {
            Assert.Equal(0, rootDirectory.GetDirectories().Length);
            Assert.Equal(0, rootDirectory.GetFiles().Length);
        }

        [Fact]
        public void Retrieve_The_Most_Recent_PackageTree_Structure_From_SVN_And_Load_To_Local_Horn_Directory()
        {
            Assert.True(rootDirectory.GetDirectories().Length > 1);
        }
    }

    public class When_CreateDefaultTreeStructure_Is_Executed_On_An_Existing_Install : PackageTreeSpecificationBase
    {
        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);
        }

        //If PackageTree Exists Do Nothing

    }

    public class When_A_PackageTree_Node_Contains_A_Build_File : PackageTreeSpecificationBase
    {
        private IPackageTree hornTree;

        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);
            hornTree = new PackageTree(rootDirectory, null);
        }

        [Fact]
        public void Then_The_Node_Will_have_a_Child()
        {
            Assert.Equal(1, hornTree.Children[0].Children.Length);
        }
        
        [Fact]
        public void Then_The_Node_Is_A_Build_Node()
        {
            Assert.True(hornTree.Children[0].Children[0].IsBuildNode);
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

    public class When_Build_Nodes_Are_Requested : PackageTreeSpecificationBase
    {
        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);
        }

        [Fact]
        public void Then_A_List_Of_Build_Nodes_Are_Returned()
        {
            IPackageTree hornTree = new PackageTree(rootDirectory, null);

            Assert.True(hornTree.BuildNodes().Count > 0);

            Assert.Equal("horn", hornTree.BuildNodes()[0].Name);
        }
    }

    public class When_Retrieve_Does_Not_Return_A_Package : PackageTreeSpecificationBase
    {
        private IPackageTree hornTree;

        protected override void Because()
        {
            rootDirectory = new DirectoryInfo(root);

            hornTree = new PackageTree(rootDirectory, null);
        }
        
        [Fact]
        public void Then_A_Null_Package_Tree_Object_Is_Returned()
        {
            Assert.IsType<NullPackageTree>(hornTree.Retrieve("unkownpackage"));
        }

        [Fact]
        public void Then_A_Null_Build_Meta_Data_Object_Is_Returned()
        {
            Assert.IsType<NullBuildMetatData>(hornTree.Retrieve("unkonwnpackage").GetBuildMetaData());
        }
    }
}