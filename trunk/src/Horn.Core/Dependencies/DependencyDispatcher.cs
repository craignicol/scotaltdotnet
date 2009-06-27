using System;
using System.Collections.Generic;
using System.IO;
using Horn.Core.BuildEngines;
using Horn.Core.extensions;
using Horn.Core.PackageStructure;
using System.Linq;

namespace Horn.Core.Dependencies
{
    public class DependencyDispatcher : IDependencyDispatcher
    {
        private readonly IDependentUpdaterExecutor dependentUpdater;
        private readonly DependencyCopier dependentCopier;
        private static string[] AllowedExtensions = new string[]
                                                        {".exe", ".dll", ".pdb", ".exe.config", ".config", ".resources", ".rsp"};

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

            var files =  dependencyDirectory.GetFiles(string.Format("{0}.*", dependency.Library));

            var ret = new List<FileInfo>();

            files.ForEach(file =>
                              {
                                  var extension =
                                      Path.GetFileName(file.FullName).Substring(dependency.Library.Length);

                                  if (AllowedExtensions.Contains(extension))
                                      ret.Add(file);
                              });

            return ret.ToArray();
        }

        private IPackageTree GetPackageForCurrentDependency(IPackageTree packageTree, Dependency dependency)
        {
            return packageTree.RetrievePackage(dependency.PackageName);
        }

        public DependencyDispatcher(IDependentUpdaterExecutor dependentUpdater)
        {
            this.dependentUpdater = dependentUpdater;
            dependentCopier = new DependencyCopier();
        }

    }
}