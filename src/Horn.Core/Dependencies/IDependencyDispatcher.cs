namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using BuildEngines;
    using PackageStructure;

    public interface IDependencyDispatcher
    {
        void Dispatch(IPackageTree packageTree, IEnumerable<Dependency> dependencies, string dependenciesRoot);
    }
}