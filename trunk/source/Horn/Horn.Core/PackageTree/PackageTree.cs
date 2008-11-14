using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.dsl;

namespace Horn.Core.PackageStructure
{
    public class PackageTree : IPackageTree
    {
        private readonly IList<IPackageTree> children;

        private IPackageTree Root
        {
            get
            {
                if (IsRoot)
                    return this;

                IPackageTree parent = Parent;

                while(parent != null)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

        public string Name{ get; private set; }

        public bool IsRoot
        {
            get { return (Parent == null); }
        }

        public IPackageTree Retrieve(string packageName)
        {
            var result = Root.GetAllPackages()
                .Where(c => c.Name == packageName).ToList();

            if (result.Count() > 0)
                return result.First();

            return null;
        }

        public BuildMetaData GetBuildMetaData()
        {
            var reader = IoC.Resolve<IBuildConfigReader>();

            return reader.SetDslFactory(CurrentDirectory).GetBuildMetaData();
        }

        public DirectoryInfo CurrentDirectory{ get; private set; }

        public void Add(IPackageTree item)
        {
            item.Parent = this;

            children.Add(item);
        }

        public void Remove(IPackageTree item)
        {
            children.Remove(item);

            item.Parent = null;
        }

        public IPackageTree Parent { get; set; }

        public IList<IPackageTree> Children
        {
            get { return children; }
        }

        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            foreach (var child in directory.GetDirectories())
            {
                children.Add(new PackageTree(new DirectoryInfo(child.FullName), this));
            }
        }
    }

    public static class PackageTreeExtensions
    {
        public static IEnumerable<IPackageTree> GetAllPackages(this IPackageTree parent)
        {
            foreach (var child in parent.Children)
            {
                yield return child;

                foreach (var descendant in child.GetAllPackages())
                {
                    yield return descendant;
                }
            }
        }
    }
}