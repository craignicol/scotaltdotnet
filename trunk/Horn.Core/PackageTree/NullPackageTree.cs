using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;

namespace Horn.Core.PackageStructure
{
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

        public IPackageTree Retrieve(string packageName)
        {
            throw new System.NotImplementedException();
        }

        public IBuildMetaData GetBuildMetaData()
        {
            return new NullBuildMetatData();
        }

        public DirectoryInfo CurrentDirectory
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
    }
}