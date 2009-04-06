using Horn.Domain.PackageStructure;
using Horn.Domain.SCM;

namespace Horn.Core.GetOperations
{
    public interface IGet
    {
        IGet Package(Package packageToGet);

        IGet From(SourceControl sourceControlToGetFrom);
        
        IPackageTree ExportTo(IPackageTree packageTree);
    }
}