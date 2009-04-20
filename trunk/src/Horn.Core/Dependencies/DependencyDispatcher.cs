namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using System.IO;
    using BuildEngines;
    using extensions;
    using PackageStructure;

    public class DependencyDispatcher : IDependencyDispatcher
    {
        private readonly IDependentUpdaterExecutor dependentUpdater;
        private readonly DependencyCopier dependentCopier;

        public DependencyDispatcher(IDependentUpdaterExecutor dependentUpdater)
        {
            this.dependentUpdater = dependentUpdater;
            dependentCopier = new DependencyCopier();
        }

        public void Dispatch(IPackageTree packageTree, IEnumerable<Dependency> dependencies, string dependenciesRoot)
        {
            DirectoryInfo dependencyDirectory = GetDependencyDirectory(packageTree, dependenciesRoot);

            foreach (Dependency dependency in dependencies)
            {
                IPackageTree packageForCurrentDependency = GetPackageForCurrentDependency(packageTree, dependency);
                FileInfo[] sourceFiles = GetDependencyFiles(dependency, packageForCurrentDependency);

                foreach (FileInfo nextFile in sourceFiles)
                {
                    IEnumerable<string> dependencyPaths = dependentCopier.CopyDependency(nextFile, dependencyDirectory);
                    dependentUpdater.Execute(packageTree, dependencyPaths, dependency);
                }
            }
        }

        private DirectoryInfo GetDependencyDirectory(IPackageTree packageTree, string targetPath)
        {
            return packageTree.WorkingDirectory.GetDirectoryFromParts(targetPath);
        }

        private FileInfo[] GetDependencyFiles(Dependency dependency, IPackageTree packageForCurrentDependency)
        {
            DirectoryInfo dependencyDirectory = packageForCurrentDependency.OutputDirectory;

            return dependencyDirectory.GetFiles(string.Format("{0}.*", dependency.Library));
        }

        private IPackageTree GetPackageForCurrentDependency(IPackageTree packageTree, Dependency dependency)
        {
            return packageTree.RetrievePackage(dependency.PackageName);
        }

        
    }
}