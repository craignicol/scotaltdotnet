using System.IO;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Domain.PackageStructure;
using Horn.Domain.SCM;
using Horn.Framework.helpers;
using Xunit;
namespace Horn.Domain.Integration.MetaDataSynchroniserFixtures
{
    public class When_the_package_tree_does_not_exist_on_the_users_machine : TestBase
    {
        private IMetaDataSynchroniser metaDataSynchroniser;

        private readonly string rootPath = DirectoryHelper.GetTempDirectoryName();

        private IPackageTree packageTree;

        protected override void Before_each_spec()
        {
            metaDataSynchroniser = new MetaDataSynchroniser(new SVNSourceControl(MetaDataSynchroniser.PACKAGE_TREE_URI));
        }

        protected override void After_each_spec()
        {
            if (Directory.Exists(rootPath))
                Directory.Delete(rootPath, true);
        }

        protected override void Because()
        {
            packageTree = new PackageTree(new DirectoryInfo(rootPath), null);

            metaDataSynchroniser.SynchronisePackageTree(packageTree);
        }

        [Fact]
        public void Then_the_package_tree_should_be_downloaded_to_the_remote_repository()
        {
            Assert.True(packageTree.Exists);
        }
    }
}