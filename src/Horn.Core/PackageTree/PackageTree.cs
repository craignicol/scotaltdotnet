using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.Dsl;
using Horn.Core.Utils;
namespace Horn.Core.PackageStructure
{
    public class PackageTree : IPackageTree
    {

        private readonly IList<IPackageTree> children;
        private DirectoryInfo workingDirectory;
        private readonly static string[] reservedDirectoryNames = new[]{"working", "output"};
        private static readonly string[] libraryNodes = new[] {"rubylib", "lib", "debug"};


        public bool Exists
        {
            get
            {
                if (!CurrentDirectory.Exists)
                    return false;

                return RootDirectoryContainsBuildFiles() > 0;
            }
        }

        private int RootDirectoryContainsBuildFiles()
        {
            //HACK: Basic check for now.  Could be expanded for a core set of required build.boo files
            return (WorkingDirectory.GetFiles("Horn.*", SearchOption.AllDirectories).Length);
        }

        public Dictionary<string, string> BuildFiles
        {
            get; set;
        }

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

        public IRevisionData GetRevisionData()
        {
            return new RevisionData(this);
        }

        public IBuildMetaData GetBuildMetaData(string packageName, string buildFile)
        {
            IPackageTree packageTree = RetrievePackage(packageName);

            return GetBuildMetaData(packageTree, buildFile);
        }

        public IBuildMetaData GetBuildMetaData(string packageName)
        {
            return GetBuildMetaData(this, packageName);
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



        private IBuildMetaData GetBuildMetaData(IPackageTree packageTree, string packageName)
        {
            var buildFileResolver = new BuildFileResolver().Resolve(packageTree.CurrentDirectory, packageName);

            var reader = IoC.Resolve<IBuildConfigReader>(buildFileResolver.Extension);

            return reader.SetDslFactory(packageTree).GetBuildMetaData(packageTree, packageName);
        }

        private PackageTree CreateNewPackageTree(DirectoryInfo child)
        {
            return new PackageTree(child, this);
        }

        private bool IsReservedDirectory(DirectoryInfo child)
        {
            return reservedDirectoryNames.Contains(child.Name.ToLower());
        }

        public virtual void CreateRequiredDirectories()
        {
            WorkingDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "Working"));
            WorkingDirectory.Create();

            OutputDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "Output"));
            OutputDirectory.Create();
        }

        private void RecordFiles()
        {
            CurrentDirectory.GetFiles("*.boo").ForEach(AddBuildFile);

            CurrentDirectory.GetFiles("*.rb").ForEach(AddBuildFile);

        }

        private void AddBuildFile(FileInfo fileInfo)
        {
            BuildFiles.Add(Path.GetFileNameWithoutExtension(fileInfo.FullName), fileInfo.FullName.Substring(0, fileInfo.FullName.LastIndexOf(".")));
        }

        private bool DirectoryIsBuildNode(DirectoryInfo directory)
        {
            if (IsRoot)
                return false;

            return (((directory.GetFiles("*.boo").Length > 0) || (directory.GetFiles("*.rb").Length > 0)) &&
                   (!libraryNodes.Contains(directory.Name.ToLower())));
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

        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            BuildFiles = new Dictionary<string, string>();
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            IsBuildNode = DirectoryIsBuildNode(directory);

            if(IsBuildNode)
            {
                RecordFiles();
                CreateRequiredDirectories();
            }

            foreach (var child in directory.GetDirectories())
            {
                if (IsReservedDirectory(child))
                    return;

                children.Add(CreateNewPackageTree(child));
            }
        }

    }
}