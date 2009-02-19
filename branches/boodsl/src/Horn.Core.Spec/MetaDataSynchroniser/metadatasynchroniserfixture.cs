using System;
using System.IO;
using Horn.Framework.helpers;
using Horn.Core.Spec.Unit.Get;
using Horn.Core.Tree.MetaDataSynchroniser;
using Xunit;

namespace Horn.Core.Spec.MetaSynchroniserfixture
{
    public class When_the_package_tree_root_directory_does_not_exist : MetaSynchroniserFixtureBase
    {
        [Fact]
        public void Then_the_meta_data_synchroniser_returns_false()
        {
            Assert.False(metaDataSynchroniser.PackageTreeExists(rootPath));
        }
    }

    public class When_the_package_tree_root_directory_exists_but_there_are_no_build_files : MetaSynchroniserFixtureBase
    {
        [Fact]
        public void Then_the_meta_data_synchroniser_returns_false()
        {
            Assert.False(metaDataSynchroniser.PackageTreeExists(rootPath));
        }        
    }

    public class When_the_root_directory_exists_and_contains_build_files :MetaSynchroniserFixtureBase
    {
        protected override void Because()
        {
            Directory.CreateDirectory(rootPath);

            var buildFile = Path.Combine(rootPath, "build.boo");

            FileHelper.CreateFileWithRandomData(buildFile);
        }

        [Fact]
        public void Then_the_meta_data_synchroniser_returns_true()
        {
            Assert.True(metaDataSynchroniser.PackageTreeExists(rootPath));
        }           
    }

    public class When_the_package_tree_structure_does_not_exist : MetaSynchroniserFixtureBase
    {
        protected override void Because()
        {
            metaDataSynchroniser.SynchronisePackageTree(rootPath);
        }

        [Fact]
        public void Then_horn_creates_the_root_folder()
        {
            Assert.True(Directory.Exists(rootPath));
        }

        [Fact]
        public void Then_the_package_tree_contains_more_than_one_build_file()
        {
            var files = Directory.GetFiles(rootPath, "build.boo", SearchOption.AllDirectories);

            Assert.True(files.Length > 0);
        }
    }
}