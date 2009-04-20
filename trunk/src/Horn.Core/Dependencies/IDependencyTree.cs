namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using PackageStructure;

    public interface IDependencyTree : IEnumerable<IPackageTree>
    {
        IList<IPackageTree> BuildList { get; }
    }
}