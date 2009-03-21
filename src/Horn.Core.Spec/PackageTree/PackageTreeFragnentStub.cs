using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.PackageStructure;

namespace Horn.Core.Spec.Unit.HornTree
{
    public class PackageTreeFragnentStub : IPackageTree
    {

        private Dictionary<string, string> buildFiles = new Dictionary<string, string>();


        public PackageTreeFragnentStub()
        {
            buildFiles.Add("horn", "horn");
        }

        public Dictionary<string, string> BuildFiles { get; set; }

        public IPackageTree[] Children
        {
            get { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo CurrentDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsBuildNode
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsRoot
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Name
        {
            get { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo OutputDirectory
        {
            get { throw new System.NotImplementedException(); }
        }

        public IPackageTree Parent
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public DirectoryInfo WorkingDirectory
        {
            get { return new DirectoryInfo(@"C:\"); }
        }



        public void Add(IPackageTree item)
        {
            throw new System.NotImplementedException();
        }

        public List<IPackageTree> BuildNodes()
        {
            throw new System.NotImplementedException();
        }

        public IBuildMetaData GetBuildMetaData(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IPackageTree item)
        {
            throw new System.NotImplementedException();
        }

        public IPackageTree Retrieve(string packageName)
        {
            throw new System.NotImplementedException();
        }



    }
}