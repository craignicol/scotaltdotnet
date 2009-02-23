using System.IO;
using Horn.Core.SCM;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Framework.helpers;
using Xunit;
namespace Horn.Core.Integration.MetaDataSynchroniserFixtures
{
    public class When_the_package_tree_does_not_exist_on_the_users_machine : TestBase
    {
        private IMetaDataSynchroniser metaDataSynchroniser;

        private readonly string rootPath = DirectoryHelper.GetTempDirectoryName();

        protected override void Before_each_spec()
        {
            metaDataSynchroniser = new MetaDataSynchroniser(new SVNSourceControl(MetaDataSynchroniser.PACKAGE_TREE_URI));
        }

        protected override void After_each_spec()
        {
            if(Directory.Exists(rootPath))
                Directory.Delete(rootPath, true);
        }

        protected override void Because()
        {
            metaDataSynchroniser.SynchronisePackageTree(rootPath);
        }

        [Fact]
        public void Then_the_package_tree_should_be_downloaded_to_the_remote_repository()
        {
            Assert.True(metaDataSynchroniser.PackageTreeExists(rootPath));
        }
    }
}