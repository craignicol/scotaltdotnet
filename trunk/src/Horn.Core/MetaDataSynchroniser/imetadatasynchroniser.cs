using System.IO;

namespace Horn.Core.Tree.MetaDataSynchroniser
{
    public interface IMetaDataSynchroniser
    {
        void SynchronisePackageTree(string rootPath);
        bool PackageTreeExists(string rootPath);
    }
}