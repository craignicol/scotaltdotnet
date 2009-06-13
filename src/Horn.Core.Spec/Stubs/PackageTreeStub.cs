using System;
using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.PackageStructure;

namespace Horn.Core.Spec.Doubles
{
    public class PackageTreeStub : IPackageTree
    {
        private readonly IBuildMetaData buildMetaData;
        private readonly string name;
        private readonly bool useInternalDictionary;
        private readonly Dictionary<string, IPackageTree> dependencyTrees = new Dictionary<string, IPackageTree>();

        public void Add(IPackageTree item)
        {
            throw new NotImplementedException();
        }

        public void AddDependencyPackageTree(string dependencyName, PackageTreeStub dependencyTree)
        {
            dependencyTrees.Add(dependencyName, dependencyTree);
        }

        public void Remove(IPackageTree item)
        {
            throw new NotImplementedException();
        }

        public IPackageTree Parent
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IPackageTree[] Children
        {
            get { throw new NotImplementedException(); }
        }

        public IPackageTree Root
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { return name; }
        }

        public bool IsRoot
        {
            get { throw new NotImplementedException(); }
        }

        public bool Exists
        {
            get { throw new NotImplementedException(); }
        }

        public string BuildFile
        {
            get { return "defaul.build"; }
        }

        public void CreateRequiredDirectories()
        {
            throw new NotImplementedException();
        }

        public IPackageTree RetrievePackage(string packageName)
        {
            if (!useInternalDictionary)
                return this;

            return dependencyTrees[packageName];
        }

        public IBuildMetaData BuildMetaData
        {
            get { return buildMetaData; }
        }

        public IBuildMetaData GetBuildMetaData(string packageName)
        {
            return buildMetaData;
        }

        public IPackageTree GetRootPackageTree(DirectoryInfo rootFolder)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo CurrentDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public FileInfo Nant
        {
            get { throw new NotImplementedException(); }
        }

        public FileInfo Sn
        {
            get { throw new NotImplementedException(); }
        }

        public DirectoryInfo WorkingDirectory
        {
            get { return new DirectoryInfo(Path.Combine(BaseDirectory, "working")).Parent; }
        }

        public bool IsBuildNode
        {
            get { throw new NotImplementedException(); }
        }

        public DirectoryInfo OutputDirectory
        {
            get { return new DirectoryInfo(Path.Combine(BaseDirectory, "output")); }
            set { throw new NotImplementedException(); }
        }

        public string BaseDirectory
        {
            get; private set;
        }

        public List<IPackageTree> BuildNodes()
        {
            return new List<IPackageTree>{this};
        }

        public IRevisionData GetRevisionData()
        {
            throw new NotImplementedException();
        }

        public PackageTreeStub(IBuildMetaData buildMetaData, string name, bool useInternalDictionary)
        {
            this.buildMetaData = buildMetaData;
            this.name = name;
            this.useInternalDictionary = useInternalDictionary;
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        public PackageTreeStub(string baseDirectory)
        {
            BaseDirectory = baseDirectory;
        }
    }
}