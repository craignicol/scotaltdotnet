using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.Dsl;
using Horn.Core.Utils;
using IronRuby.Builtins;

namespace Horn.Core.PackageStructure
{
    public class PackageTree : IPackageTree
    {

        private readonly IList<IPackageTree> children;
        private DirectoryInfo workingDirectory;
        private readonly static string[] reservedDirectoryNames = new[]{"working", "output"};
        private static readonly string[] libraryNodes = new[] {"rubylib", "lib", "debug", "buildengines"};


        public string BuildFile{ get; set; }

        public IBuildMetaData BuildMetaData{ get; private set; }

        public IPackageTree[] Children
        {
            get { return children.ToArray(); }
        }

        public DirectoryInfo CurrentDirectory { get; private set; }

        public bool Exists
        {
            get
            {
                if (!CurrentDirectory.Exists)
                    return false;

                return RootDirectoryContainsBuildFiles() > 0;
            }
        }

        public bool IsBuildNode { get; private set; }

        public bool IsRoot
        {
            get { return (Parent == null); }
        }

        public string Name{ get; private set; }

        public FileInfo Nant
        {
            get
            {
                //TODO: Find a less explicit way to find the nant.exe
                var path = Path.Combine(Root.CurrentDirectory.FullName, "buildengines");
                path = Path.Combine(path, "Nant");
                path = Path.Combine(path, "Nant");
                path = Path.Combine(path, "NAnt.exe");

                return new FileInfo(path);
            }
        }

        public DirectoryInfo OutputDirectory { get; private set; }

        public IPackageTree Parent { get; set; }

        public FileInfo Sn
        {
            get
            {
                //TODO: Find a less explicit way to find the sn.exe
                var path = Path.Combine(Root.CurrentDirectory.FullName, "buildengines");
                path = Path.Combine(path, "Sn");
                path = Path.Combine(path, "sn.exe");

                return new FileInfo(path);                
            }
        }

        public DirectoryInfo WorkingDirectory
        {
            get { return IsRoot ? CurrentDirectory : workingDirectory; }
            private set { workingDirectory = value; }
        }



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

        public virtual void CreateRequiredDirectories()
        {
            WorkingDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "Working"));
            WorkingDirectory.Create();

            OutputDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "Output"));
            OutputDirectory.Create();
        }

        public IBuildMetaData GetBuildMetaData(string packageName)
        {
            IPackageTree packageTree = RetrievePackage(packageName);

            return GetBuildMetaData(packageTree);
        }

        public IRevisionData GetRevisionData()
        {
            return new RevisionData(this);
        }

        public void Remove(IPackageTree item)
        {
            children.Remove(item);

            item.Parent = null;
        }

        public IPackageTree RetrievePackage(string packageName)
        {
            var result = Root.GetAllPackages()
                .Where(c => c.Name == packageName).ToList();

            if (result.Count() == 0)
                return new NullPackageTree();
                
            return result.First();
        }



        private int RootDirectoryContainsBuildFiles()
        {
            //HACK: Basic check for now.  Could be expanded for a core set of required build.boo files
            return (WorkingDirectory.GetFiles("horn.*", SearchOption.AllDirectories).Length);
        }

        private IBuildMetaData GetBuildMetaData(IPackageTree packageTree)
        {
            if (BuildMetaData != null)
                return BuildMetaData;

            var buildFileResolver = new BuildFileResolver().Resolve(packageTree.CurrentDirectory, packageTree.Name);

            var reader = IoC.Resolve<IBuildConfigReader>(buildFileResolver.Extension);

            BuildMetaData = reader.SetDslFactory(packageTree).GetBuildMetaData(packageTree, Path.GetFileNameWithoutExtension(buildFileResolver.BuildFile));

            return BuildMetaData;
        }

        private PackageTree CreateNewPackageTree(DirectoryInfo child)
        {
            return new PackageTree(child, this);
        }

        private bool IsReservedDirectory(DirectoryInfo child)
        {
            return reservedDirectoryNames.Contains(child.Name.ToLower());
        }

        private bool DirectoryIsBuildNode(DirectoryInfo directory)
        {
            if (IsRoot)
                return false;

            return (directory.GetFiles("*.boo").Length > 0) &&
                   (!libraryNodes.Contains(directory.Name.ToLower()));
        }



        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            IsBuildNode = DirectoryIsBuildNode(directory);

            if(IsBuildNode)
            {
                CreateRequiredDirectories();
            }

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

                while (!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

    }
}