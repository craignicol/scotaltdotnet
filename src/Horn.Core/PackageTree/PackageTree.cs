using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.dsl;
using Horn.Core.SCM;

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
        public static void CreateDefaultTreeStructure(string rootPath)
        {
            var root = new DirectoryInfo(rootPath);
            
            if (!root.Exists) Directory.CreateDirectory(rootPath);
            if (root.GetDirectories().Length != 0) return;

            var svn = new SVNSourceControl("http://scotaltdotnet.googlecode.com/svn/trunk/package_tree/");
            svn.Export(rootPath);
        }

        //HACK: replace with synchronisation from svn, http or ftp.  Very, very temporary measure
        public static void CreateDefaultTreeStructure(string hornBuildFile, string sourceBuildFile)
        {
            CreateDirectory(hornBuildFile);

            string builders = CreateDirectory(hornBuildFile, "builders");
            string horn = CreateDirectory(builders, "horn");

            CopyBuildFileToDestination(sourceBuildFile, horn);

            string loggers = CreateDirectory(hornBuildFile, "loggers");
            string log4net = CreateDirectory(loggers, "log4net");

            var log4NetBuildFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.boo");

            CopyBuildFileToDestination(log4NetBuildFile, log4net);
        }

        private static void CopyBuildFileToDestination(string sourceFile, string destinationFolder)
        {
            if(!File.Exists(sourceFile))
                throw new FileNotFoundException(string.Format("The build file {0} does not exist", sourceFile));

            string destinationBuildFile = Path.Combine(destinationFolder, "build.boo");

            File.Copy(sourceFile, destinationBuildFile, true);
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

        private static string CreateDirectory(string directoryPath, string newDirectoryName)
        {
            var combination = Path.Combine(directoryPath, newDirectoryName);

            return CreateDirectory(combination);
        }

        private static string CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);

            return directoryPath;
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