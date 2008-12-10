using Horn.Core.SCM;

namespace Horn.Core.GetOperations
{
    public interface IGet
    {
        IGet Package(Package packageToGet);
        IGet From(SourceControl sourceControlToGetFrom);
        string ExportTo(string path);
    }
}