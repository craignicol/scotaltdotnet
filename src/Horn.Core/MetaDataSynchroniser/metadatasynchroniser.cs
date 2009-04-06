using System.IO;
using Horn.Domain.PackageStructure;
using Horn.Domain.SCM;

namespace Horn.Core.Tree.MetaDataSynchroniser
{
    public class MetaDataSynchroniser : IMetaDataSynchroniser
    {

        private readonly SourceControl sourceControl;
        public const string PACKAGE_TREE_URI = "http://scotaltdotnet.googlecode.com/svn/trunk/package_tree/";


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