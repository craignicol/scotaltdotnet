namespace Horn.Core.Get
{
    public interface IGet
    {
        IGet Project(Project projectToGet);
        IGet From(SourceControl sourceControlToGetFrom);
        string Export();
    }
}