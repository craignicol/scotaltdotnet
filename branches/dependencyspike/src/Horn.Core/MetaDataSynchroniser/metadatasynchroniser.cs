using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;

namespace Horn.Core.Tree.MetaDataSynchroniser
{
    public class MetaDataSynchroniser : IMetaDataSynchroniser
    {

        private readonly SourceControl sourceControl;
        public const string PACKAGE_TREE_URI = "http://scotaltdotnet.googlecode.com/svn/branches/dependencyspike/package_tree/";


        public void SynchronisePackageTree(IPackageTree packageTree)
        {
            sourceControl.Export(packageTree);
        }



        public MetaDataSynchroniser(SourceControl sourceControl)
        {
            this.sourceControl = sourceControl;
        }



    }

}