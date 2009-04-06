namespace Horn.Domain.PackageStructure
{
    public interface IRevisionData
    {
        void RecordRevision(IPackageTree packageTree, string revisionVlaue);

        string Revision { get; }

        bool ShouldUpdate(IRevisionData other);
    }
}