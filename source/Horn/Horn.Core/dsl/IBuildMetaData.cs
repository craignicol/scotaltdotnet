namespace Horn.Core.dsl
{
    public interface IBuildMetaData
    {
        BuildEngine BuildEngine { get; set; }
        SourceControl SourceControl { get; set; }
    }
}