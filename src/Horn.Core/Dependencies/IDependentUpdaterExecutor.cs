namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using BuildEngines;
    using PackageStructure;

    public interface IDependentUpdaterExecutor
    {
        void Execute(IPackageTree packageTree, IEnumerable<string> dependencyPaths, Dependency dependency);
    }
}