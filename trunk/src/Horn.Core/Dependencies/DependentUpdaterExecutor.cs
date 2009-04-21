namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using BuildEngines;
    using extensions;
    using PackageStructure;
    using Utils;

    public class DependentUpdaterExecutor :  WithLogging, IDependentUpdaterExecutor
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

            InfoFormat("Dependency: Executing Dependency Updaters for {0}", packageTree.Name);

            var dependentUpdaterContext = new DependentUpdaterContext(packageTree, dependencyPaths, dependency);
            updaters.ForEach(updater => updater.Update(dependentUpdaterContext));
        }

        private bool HasADependencyToUpdate(IEnumerable<string> dependencyPaths)
        {
            return dependencyPaths != null && dependencyPaths.Count() > 0;
        }
    }
}