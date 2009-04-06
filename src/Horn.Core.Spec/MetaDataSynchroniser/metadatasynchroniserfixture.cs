using System.IO;
using Horn.Domain.PackageStructure;
using Horn.Domain.Spec.helpers;
using Xunit;

namespace Horn.Domain.Spec.MetaSynchroniserfixture
{

    public class When_the_package_tree_structure_does_not_exist : MetaSynchroniserFixtureBase
    {
        private IPackageTree packageTree;

        protected override void Because()
        {
            packageTree = TreeHelper.GetTempEmptyPackageTree();

            metaDataSynchroniser.SynchronisePackageTree(packageTree);
        }

        [Fact]
        public void Then_horn_creates_the_root_folder()
        {
            Assert.True(packageTree.CurrentDirectory.Exists);
        }

        [Fact]
        public void Then_the_package_tree_contains_more_than_one_build_file()
        {
            var files = packageTree.CurrentDirectory.GetFiles("horn.*", SearchOption.AllDirectories);

            Assert.True(files.Length > 0);
        }
    }
}