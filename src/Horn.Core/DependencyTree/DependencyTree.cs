namespace Horn.Core.DependencyTree
{
    using System;
    using System.Collections.Generic;
    using Horn.Core.dsl;
    using Horn.Core.PackageStructure;
    using Horn.Core.BuildEngines;

    public class DependencyTree : IDependencyTree
    {
        private class BuildTree
        {
            private IList<BuildTree> Children;
            private BuildTree Parent;
            private IPackageTree Node;
            public BuildTree(IPackageTree node)
                : this(node, new List<BuildTree>(), null)
		{
		}
            public BuildTree(IPackageTree node, BuildTree parent)
		: this(node, new List<BuildTree>(), parent)
		{
		}
            public BuildTree(IPackageTree node, IList<BuildTree> children)
		: this(node, children, null)
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
                else
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

        public DependencyTree(IPackageTree packageTree)
        {
            PackageTree = packageTree;

            CalculateDependencies(PackageTree);
        }

        public IPackageTree PackageTree { get; private set; }
        private BuildTree buildTree;
        public IList<IPackageTree> BuildList
        {
            get
            {
            		return buildTree.GetBuildList();
            }
        }

        private void CalculateDependencies(IPackageTree packageTree)
        {
            buildTree = CalculateDependencies(packageTree, null);
        }

        private BuildTree CalculateDependencies(IPackageTree packageTree, BuildTree tree)
        {
            if (tree == null)
            {
                tree = new BuildTree(packageTree);
            }
            else
            {
                if (tree.GetAncestors().Contains(packageTree))
                {
                    throw new CircularDependencyException(packageTree.Name);
                }
                // Insert dependencies before parents
                tree.AddChild(packageTree);
            }
            packageTree.GetBuildMetaData().BuildEngine.Dependencies.ForEach(
                dependency => CalculateDependencies(PackageTree.Retrieve(dependency.Name), tree));

            return tree;

        }
    }
}
