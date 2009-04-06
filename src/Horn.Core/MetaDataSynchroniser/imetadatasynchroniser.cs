using Horn.Domain.PackageStructure;

namespace Horn.Core.Tree.MetaDataSynchroniser
{
    public interface IMetaDataSynchroniser
    {
        void SynchronisePackageTree(IPackageTree packageTree);
    }
}