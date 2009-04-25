using System.Collections.Generic;
using Horn.Core.PackageStructure;

namespace Horn.Core.Dependencies
{
    public class BuildTree
    {
        private IList<BuildTree> Children;
        private BuildTree Parent;
        private IPackageTree Node;        

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

        public BuildTree(IPackageTree node)
            : this(node, new List<BuildTree>(), null)
        {
        }

        public BuildTree(IPackageTree node, BuildTree parent)
            : this(node, new List<BuildTree>(), parent)
        {
        }

        public BuildTree(IPackageTree node, IList<BuildTree> children, BuildTree parent)
        {
            Parent = parent;
            Children = children;
            Node = node;
        }

    }
}