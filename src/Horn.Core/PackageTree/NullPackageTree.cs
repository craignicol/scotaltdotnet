using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;

namespace Horn.Core.PackageStructure
{
    using System;

    public class NullPackageTree : IPackageTree
    {
        public void Add(IPackageTree item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IPackageTree item)
        {
            throw new System.NotImplementedException();
        }

        public IPackageTree Parent
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IPackageTree[] Children
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Name
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsRoot
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Exists
        {
            get { throw new System.NotImplementedException(); }
        }

        public string BuildFile
        {
            get { throw new System.NotImplementedException(); }
        }

        public Dictionary<string, string> BuildFiles { get; set; }

        public void CreateRequiredDirectories()
        {
            throw new System.NotImplementedException();
        }

        public IPackageTree RetrievePackage(string packageName)
        {
            throw new System.NotImplementedException();
        }

        public IBuildMetaData BuildMetaData
        {
            get { throw new System.NotImplementedException(); }
        }

        public IBuildMetaData GetBuildMetaData(string packageName)
        {
            return new NullBuildMetatData();
        }

        public IBuildMetaData GetBuildMetaData(string packageName, string buildFile)
        {
            throw new System.NotImplementedException();
        }

        public DirectoryInfo CurrentDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public FileInfo Nant
        {
            get { throw new System.NotImplementedException(); }
        }

        public FileInfo Sn
        {
            get { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo DownloadDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo WorkingDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsBuildNode
        {
            get { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo OutputDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public List<IPackageTree> BuildNodes()
        {
            throw new System.NotImplementedException();
        }

        public IRevisionData GetRevisionData()
        {
            throw new System.NotImplementedException();
        }
    }
}