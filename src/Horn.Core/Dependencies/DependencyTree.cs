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
                return buildTree.GetBuildList();
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
            packageTree.GetBuildMetaData(packageTree.Name)
                .BuildEngine
                .Dependencies
                .ForEach(
                dependency =>
                CalculateDependencies
                    (
                    PackageTree.RetrievePackage(dependency.PackageName),
                    currentTree)
                );

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



        private class BuildTree
        {
            private IList<BuildTree> Children;
            private BuildTree Parent;
            private IPackageTree Node;
            
            public BuildTree(IPackageTree node): this(node, new List<BuildTree>(), null)
            {
            }

            public BuildTree(IPackageTree node, BuildTree parent) : this(node, new List<BuildTree>(), parent)
            {
            }

            public BuildTree(IPackageTree node, IList<BuildTree> children, BuildTree parent)
            {
                Parent = parent;
                Children = children;
                Node = node;                
            }

            public BuildTree AddChild(IPackageTree node) 
            {
                Children.Add(new BuildTree(node, this));
                return this;
            }

            private IList<IPackageTree> GetAncestors(IList<IPackageTree> ancestorList)
            {
                ancestorList.Add(Node);
                if (Parent != null)
                    return Parent.GetAncestors(ancestorList);
                
                return ancestorList;
            }

            public IList<IPackageTree> GetAncestors()
            {
                return GetAncestors(new List<IPackageTree>());
            }

            private IList<IPackageTree> GetBuildList(IList<IPackageTree> buildList)
            {
                foreach (BuildTree buildTree in Children)
                {
                    buildTree.GetBuildList(buildList);
                }
                buildList.Add(Node);
                return buildList;
            }

            public IList<IPackageTree> GetBuildList()
            {
                return GetBuildList(new List<IPackageTree>());
            }

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