using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.Dsl;

namespace Horn.Core.PackageStructure
{
    public class PackageTree : IPackageTree
    {
        private readonly IBuildFileExtensionResolver buildFileExtensionResolver;

        private readonly IList<IPackageTree> children;
        private readonly static string[] reservedDirectoryNames = new[]{"working", "output"};


        public IPackageTree[] Children
        {
            get { return children.ToArray(); }
        }

        public DirectoryInfo CurrentDirectory { get; private set; }

        public bool IsBuildNode { get; private set; }

        public bool IsRoot
        {
            get { return (Parent == null); }
        }

        public string Name{ get; private set; }

        public DirectoryInfo OutputDirectory { get; private set; }

        public IPackageTree Parent { get; set; }

        public DirectoryInfo WorkingDirectory{ get; private set;}



        public void Add(IPackageTree item)
        {
            item.Parent = this;

            children.Add(item);
        }

        public List<IPackageTree> BuildNodes()
        {
            var result = Root.GetAllPackages()
                .Where(c => c.IsBuildNode).ToList();

            return result;
        }

        public IBuildMetaData GetBuildMetaData()
        {
            var fileType = buildFileExtensionResolver.Resolve(CurrentDirectory);

            var reader = IoC.Resolve<IBuildConfigReader>(fileType);

            return reader.SetDslFactory(CurrentDirectory).GetBuildMetaData();
        }

        public void Remove(IPackageTree item)
        {
            children.Remove(item);

            item.Parent = null;
        }

        public IPackageTree Retrieve(string packageName)
        {
            var result = Root.GetAllPackages()
                .Where(c => c.Name == packageName).ToList();

            if (result.Count() == 0)
                return new NullPackageTree();
                
            return result.First();
        }



        private PackageTree CreateNewPackageTree(DirectoryInfo child)
        {
            return new PackageTree(child, this, buildFileExtensionResolver);
        }

        private bool IsReservedDirectory(DirectoryInfo child)
        {
            return reservedDirectoryNames.Contains(child.Name.ToLower());
        }

        private void CreateRequiredDirectories(DirectoryInfo directory)
        {
            WorkingDirectory = new DirectoryInfo(Path.Combine(directory.FullName, "Working"));
            WorkingDirectory.Create();

            OutputDirectory = new DirectoryInfo(Path.Combine(directory.FullName, "Output"));
            OutputDirectory.Create();
        }



        public PackageTree(DirectoryInfo directory, IPackageTree parent, IBuildFileExtensionResolver buildFileExtensionResolver)
        {
            this.buildFileExtensionResolver = buildFileExtensionResolver;

            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            IsBuildNode = (directory.GetFiles("*.boo").Length > 0) || (directory.GetFiles("*.rb").Length > 0);

            if(IsBuildNode)
                CreateRequiredDirectories(directory);

            foreach (var child in directory.GetDirectories())
            {
                if (IsReservedDirectory(child))
                    return;

                children.Add(CreateNewPackageTree(child));
            }
        }


        private IPackageTree Root
        {
            get
            {
                if (IsRoot)
                    return this;

                IPackageTree parent = Parent;

                while(!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

    }
}