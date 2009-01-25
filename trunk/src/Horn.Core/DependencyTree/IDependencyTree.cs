namespace Horn.Core.DependencyTree
{
    using System;
    using System.Collections.Generic;
    using Horn.Core.dsl;
    using Horn.Core.PackageStructure;

    public interface IDependencyTree
    {
        IList<IPackageTree> BuildList { get; }
    }
}
