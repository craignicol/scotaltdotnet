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
        private readonly static string[] reservedDirectoryNames = new[]{"working", "output"};


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

        public IBuildMetaData GetBuildMetaData(string fileName)
        {
            var buildFileResolver = new BuildFileResolver().Resolve(CurrentDirectory, fileName);

            var reader = IoC.Resolve<IBuildConfigReader>(buildFileResolver.Extension);

            return reader.SetDslFactory(CurrentDirectory).GetBuildMetaData(fileName);
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
            return new PackageTree(child, this);
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

        private void RecordFiles()
        {
            CurrentDirectory.GetFiles("*.boo").ForEach(AddBuildFile);

            CurrentDirectory.GetFiles("*.rb").ForEach(AddBuildFile);

        }

        private void AddBuildFile(FileInfo fileInfo)
        {
            BuildFiles.Add(Path.GetFileNameWithoutExtension(fileInfo.FullName), fileInfo.FullName.Substring(0, fileInfo.FullName.LastIndexOf(".")));
        }



        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            BuildFiles = new Dictionary<string, string>();
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            IsBuildNode = (directory.GetFiles("*.boo").Length > 0) || (directory.GetFiles("*.rb").Length > 0);

            if(IsBuildNode)
            {
                RecordFiles();
                CreateRequiredDirectories(directory);
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

                while(!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

    }
}