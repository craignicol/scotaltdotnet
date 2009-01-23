namespace Horn.Core.Spec.Unit.DependencyTree
{
    using System;
    using System.Collections.Generic;
    using Horn.Core.dsl;
    using Horn.Core.PackageStructure;
    using Horn.Core.BuildEngines;

    public class DependencyTree : IDependencyTree
    {
        public DependencyTree(IBuildMetaData buildMetaData, IPackageTree packageTree)
        {
            PackageTree = packageTree;
            BuildMetaData = buildMetaData;
            BuildList = new List<IBuildMetaData>();

            CalculateDependencies(BuildMetaData);
        }

        public IPackageTree PackageTree { get; private set; }
        public IBuildMetaData BuildMetaData { get; private set; }
        public IList<IBuildMetaData> BuildList
        {
            get;
            private set;
        }

        private void CalculateDependencies(IBuildMetaData buildMetaData)
        {
            // Dependencies go first
            buildMetaData.BuildEngine.Dependencies.ForEach(
                dependency =>
                    CalculateDependencies(PackageTree.Retrieve(dependency.Name).GetBuildMetaData()));

            BuildList.Add(buildMetaData);
        }
    }
}
