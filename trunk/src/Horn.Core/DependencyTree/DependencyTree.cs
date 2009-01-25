namespace Horn.Core.Spec.Unit.DependencyTree
{
    using System;
    using System.Collections.Generic;
    using Horn.Core.dsl;
    using Horn.Core.PackageStructure;
    using Horn.Core.BuildEngines;

    public class DependencyTree : IDependencyTree
    {

        public DependencyTree(IPackageTree packageTree)
        {
            PackageTree = packageTree;
            BuildList = new List<IPackageTree>();

            CalculateDependencies(PackageTree);
        }

        public IPackageTree PackageTree { get; private set; }
        public IList<IPackageTree> BuildList
        {
            get;
            private set;
        }

        private void CalculateDependencies(IPackageTree packageTree)
        {
            // Dependencies go first
            packageTree.GetBuildMetaData().BuildEngine.Dependencies.ForEach(
                dependency => CalculateDependencies(PackageTree.Retrieve(dependency.Name)));

            BuildList.Add(packageTree);
        }
    }
}
