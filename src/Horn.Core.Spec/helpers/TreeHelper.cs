
using Horn.Domain.PackageStructure;
using Horn.Framework.helpers;

namespace Horn.Domain.Spec.helpers
{
    public static class TreeHelper
    {
        public static IPackageTree GetTempEmptyPackageTree()
        {
            var treeDirectory = PackageTreeHelper.CreateEmptyDirectoryStructureForTesting();

            return new PackageTree(treeDirectory, null);            
        }

        public static IPackageTree GetTempPackageTree()
        {
            var treeDirectory =  PackageTreeHelper.CreateDirectoryStructureForTesting();

            return new PackageTree(treeDirectory, null);
        }
    }
}