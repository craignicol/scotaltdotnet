using Horn.Core.SCM;

namespace Horn.Core.Get
{
    public interface IGet
    {
        IGet Package(Package packageToGet);
        IGet From(SourceControl sourceControlToGetFrom);
        string Export();
    }
}