using Horn.Core.BuildEngines;
using System.Linq;
namespace Horn.Core.Dependencies
{
    using System.Collections;
    using System.Collections.Generic;
    using PackageStructure;

    public class DependencyTree : IDependencyTree
    {

        private BuildTree buildTree;

        public IList<IPackageTree> BuildList
        {
            get
            {
                var buildList = new List<IPackageTree>(buildTree.GetBuildList().OrderBy(x => x.GetBuildMetaData(x.Name).BuildEngine.Dependencies.Count));

                IPackageTree parent = null;

                for (var i = buildList.Count - 1; i >= 0; i-- )
                {
                    var tree = buildList[i];

                    if (tree.Name.ToLower().Equals(PackageTree.Name.ToLower()))
                    {
                        parent = tree;
                        buildList.Remove(tree);
                        break;
                    }
                        
                }

                buildList.Insert((buildList.Count), parent);

                return buildList;
            }
        }

        private IPackageTree PackageTree { get; set; }

        private void CalculateDependencies(IPackageTree packageTree)
        {
            buildTree = CalculateDependencies(packageTree, null);
        }

        private BuildTree CalculateDependencies(IPackageTree packageTree, BuildTree currentTree)
        {
            if (currentTree == null)
            {
                currentTree = new BuildTree(packageTree);
            }
            else
            {
                if (HasACircularDependency(currentTree, packageTree))
                {
                    throw new CircularDependencyException(packageTree.Name);
                }
                
                InsertDependenciesBeforeParent(currentTree, packageTree);
            }

            var buildMetaData = packageTree.GetBuildMetaData(packageTree.Name);

            var buildEngine = buildMetaData.BuildEngine;

            var dependencies = buildEngine.Dependencies;

            foreach (var dependency in dependencies)
            {
                var package = packageTree.RetrievePackage(dependency.PackageName);

                CalculateDependencies(package, currentTree);
            }

            return currentTree;

        }

        private void InsertDependenciesBeforeParent(BuildTree tree, IPackageTree packageTree)
        {
            tree.AddChild(packageTree);
        }

        private bool HasACircularDependency(BuildTree tree, IPackageTree packageTree)
        {
            return tree.GetAncestors().Contains(packageTree);
        }


        public DependencyTree(IPackageTree packageTree)
        {
            PackageTree = packageTree;

            CalculateDependencies(PackageTree);
        }

        public IEnumerator<IPackageTree> GetEnumerator()
        {
            return BuildList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}