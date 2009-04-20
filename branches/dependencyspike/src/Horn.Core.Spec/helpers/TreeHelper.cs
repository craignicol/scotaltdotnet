using Horn.Core.PackageStructure;
using Horn.Framework.helpers;

namespace Horn.Core.Spec.helpers
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