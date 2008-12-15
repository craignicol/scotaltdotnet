using System.Collections.Generic;
using System.IO;
using Horn.Core.dsl;
using Horn.Core.PackageStructure;

namespace Horn.Core.Spec.Unit.HornTree
{
    public class PackageTreeFragnentStub : IPackageTree
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
            throw new System.NotImplementedException();
        }

        public DirectoryInfo CurrentDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo WorkingDirectory
        {
            get { return new DirectoryInfo(@"C:\"); }
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