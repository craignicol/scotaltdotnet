namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using BuildEngines;
    using extensions;
    using PackageStructure;

    public class DependentUpdaterExecutor :  IDependentUpdaterExecutor
    {
        private readonly HashSet<IDependentUpdater> updaters;

        public DependentUpdaterExecutor(IEnumerable<IDependentUpdater> updaters)
        {
            this.updaters = new HashSet<IDependentUpdater>(updaters);
        }

        public void Execute(IPackageTree packageTree, IEnumerable<string> dependencyPaths, Dependency dependency)
        {
            if (!HasADependencyToUpdate(dependencyPaths))
                return;

            updaters.ForEach(updater => updater.Update(new DependentUpdaterContext(packageTree, dependencyPaths, dependency)));

        }

        private bool HasADependencyToUpdate(IEnumerable<string> dependencyPaths)
        {
            return dependencyPaths != null && dependencyPaths.Count() > 0;
        }
    }
}