namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using System.IO;
    using BuildEngines;
    using PackageStructure;

    public interface IDependentUpdater
    {
        void Update(DependentUpdaterContext dependentUpdaterContext);
    }

    public class DependentUpdaterContext
    {
        private readonly IPackageTree package;
        private readonly IEnumerable<string> dependencyPaths;
        private readonly Dependency dependency;

        public DependentUpdaterContext(IPackageTree package, IEnumerable<string> dependencyPaths, Dependency dependency)
        {
            this.package = package;
            this.dependencyPaths = dependencyPaths;
            this.dependency = dependency;
        }

        public DirectoryInfo WorkingDirectory
        {
            get { return package.WorkingDirectory; }
        }

        public IEnumerable<string> DependencyPaths
        {
            get { return dependencyPaths; }
        }

        public Dependency Dependency
        {
            get { return dependency; }
        }
    }
}