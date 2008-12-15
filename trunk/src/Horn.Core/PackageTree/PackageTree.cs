using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.dsl;

namespace Horn.Core.PackageStructure
{
    public class PackageTree : IPackageTree
    {
        private readonly IList<IPackageTree> children;

        private readonly static string[] reservedDirectoryNames = new[]{"working", "output"};

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

        public DirectoryInfo CurrentDirectory { get; private set; }

        public DirectoryInfo WorkingDirectory{ get; private set;}

        public bool IsBuildNode { get; private set; }

        public DirectoryInfo OutputDirectory { get; private set; }

        public IPackageTree[] Children
        {
            get { return children.ToArray(); }
        }

        public IPackageTree Parent { get; set; }

        public IPackageTree Retrieve(string packageName)
        {
            var result = Root.GetAllPackages()
                .Where(c => c.Name == packageName).ToList();

            if (result.Count() == 0)
                return new NullPackageTree();
                
            return result.First();
        }

        public IBuildMetaData GetBuildMetaData()
        {
            var reader = IoC.Resolve<IBuildConfigReader>();

            return reader.SetDslFactory(CurrentDirectory).GetBuildMetaData();
        }

        public List<IPackageTree> BuildNodes()
        {
            var result = Root.GetAllPackages()
                .Where(c => c.IsBuildNode).ToList();

            return result;
        }

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

        //HACK: replace with synchronisation from svn, http or ftp.  Very, very temporary measure
        public static void CreateDefaultTreeStructure(string rootPath, string sourceBuildFile)
        {       
            CreateDirectory(rootPath);
            string distros = CreateDistrosDirectory(rootPath);
            string horn = CreateHornDirectory(distros);

            string destinationBuildFile = CreateDestinationBuildFilePath(horn, sourceBuildFile);

            if (FileExists(destinationBuildFile))
                return;

            if (!FileExists(sourceBuildFile))
                throw new FileNotFoundException(string.Format("The following build file does not exist: {0}.", sourceBuildFile));

            CopySourceToDestination(sourceBuildFile, destinationBuildFile);
        }

        private static void CopySourceToDestination(string sourceBuildFile, string destinationBuildFile)
        {
            File.Copy(sourceBuildFile, destinationBuildFile);
        }

        private static bool FileExists(string destinationBuildFile)
        {
            return File.Exists(destinationBuildFile);
        }

        private static string CreateDestinationBuildFilePath(string horn, string sourceBuildFile)
        {
            return Path.Combine(horn, Path.GetFileName(sourceBuildFile));
        }

        private static string CreateHornDirectory(string distros)
        {
            var horn = Path.Combine(distros, "horn");

            CreateDirectory(horn);

            return horn;
        }

        private static string CreateDistrosDirectory(string rootPath)
        {
            var distros = Path.Combine(rootPath, "distros");

            CreateDirectory(distros);
            return distros;
        }

        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            IsBuildNode = (directory.GetFiles("*.boo").Length > 0);

            if(IsBuildNode)
                CreateRequiredDirectories(directory);

            foreach (var child in directory.GetDirectories())
            {
                if (IsReservedDirectory(child))
                    return;

                children.Add(CreateNewPackageTree(child));
            }
        }

        private PackageTree CreateNewPackageTree(DirectoryInfo child)
        {
            return new PackageTree(child, this);
        }

        private static void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
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
    }
}