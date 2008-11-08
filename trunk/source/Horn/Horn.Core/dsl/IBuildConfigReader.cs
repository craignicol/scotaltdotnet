namespace Horn.Core.dsl
{
    public interface IBuildConfigReader
    {
        BuildMetaData GetBuildMetaData(string source);
    }
}