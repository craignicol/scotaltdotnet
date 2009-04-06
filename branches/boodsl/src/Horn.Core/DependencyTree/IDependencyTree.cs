namespace Horn.Core.DependencyTree
{
    using System.Collections.Generic;
    using PackageStructure;

    public interface IDependencyTree
    {
        IList<IPackageTree> BuildList { get; }
    }
}
