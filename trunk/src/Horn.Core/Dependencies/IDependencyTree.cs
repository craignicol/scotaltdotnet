namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using PackageStructure;

    public interface IDependencyTree : IEnumerable<IPackageTree>
    {
        HashSet<IPackageTree> BuildList { get; }
    }
}