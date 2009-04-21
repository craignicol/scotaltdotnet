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
        private readonly IPackageTree packageTree;
        private readonly string baseDirectory;

        public void Add(IPackageTree item)
        {
            throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
        }

        public void CreateRequiredDirectories()
        {
            throw new NotImplementedException();
        }

        public IPackageTree RetrievePackage(string packageName)
        {
            if (packageTree == null)
                return this;

            return packageTree;
        }

        public IBuildMetaData BuildMetaData
        {
            get { return buildMetaData; }
        }

        public IBuildMetaData GetBuildMetaData(string packageName)
        {
            return buildMetaData;
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
            get { return new DirectoryInfo(Path.Combine(baseDirectory, "working")); }
        }

        public bool IsBuildNode
        {
            get { throw new NotImplementedException(); }
        }

        public DirectoryInfo OutputDirectory
        {
            get { return new DirectoryInfo(Path.Combine(baseDirectory, "output")); }
            set { throw new NotImplementedException(); }
        }

        public List<IPackageTree> BuildNodes()
        {
            throw new NotImplementedException();
        }

        public IRevisionData GetRevisionData()
        {
            throw new NotImplementedException();
        }

        public PackageTreeStub(IBuildMetaData buildMetaData, string name, IPackageTree packageTree)
        {
            this.buildMetaData = buildMetaData;
            this.name = name;
            this.packageTree = packageTree;
        }

        public PackageTreeStub(string baseDirectory)
        {
            this.baseDirectory = baseDirectory;
        }
    }
}