using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.dsl;
using log4net;

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

        public DirectoryInfo CurrentDirectory { get; private set; }

        public DirectoryInfo WorkingDirectory{ get; private set;}

        public bool IsBuildNode { get; private set; }

        public IList<IPackageTree> Children
        {
            get { return children; }
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

            return new List<IPackageTree>(result);
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
            if(Directory.Exists(rootPath))
                Directory.Delete(rootPath, true);

            CreateDirectory(rootPath);

            var distros = string.Format("{0}\\{1}\\", rootPath, "distros");

            CreateDirectory(distros);

            var horn = string.Format("{0}{1}\\", distros, "horn");

            CreateDirectory(horn);

            var destinationBuildFile = string.Format("{0}{1}", horn, Path.GetFileName(sourceBuildFile));

            if (File.Exists(destinationBuildFile))
                return;

            if (!File.Exists(sourceBuildFile))
                throw new FileNotFoundException(string.Format("The following build file does not exist: {0}.", sourceBuildFile));

            File.Copy(sourceBuildFile, destinationBuildFile);
        }

        private static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        } 

        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            WorkingDirectory =
                new DirectoryInfo(string.Format("{0}{1}{2}", directory.FullName, Path.DirectorySeparatorChar, "Working"));

            IsBuildNode = (directory.GetFiles("*.boo").Length > 0);

            foreach (var child in directory.GetDirectories())
            {
                children.Add(new PackageTree(new DirectoryInfo(child.FullName), this));
            }
        }
    }
}