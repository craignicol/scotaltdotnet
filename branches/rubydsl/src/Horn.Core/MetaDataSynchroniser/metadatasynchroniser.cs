using System.IO;
using Horn.Core.SCM;

namespace Horn.Core.Tree.MetaDataSynchroniser
{
    public class MetaDataSynchroniser : IMetaDataSynchroniser
    {
        private readonly SourceControl sourceControl;

        public const string PACKAGE_TREE_URI = "http://scotaltdotnet.googlecode.com/svn/trunk/package_tree/";

        public void SynchronisePackageTree(string rootPath)
        {
            if(PackageTreeExists(rootPath))
                return;

            sourceControl.Export(rootPath);
        }

        public bool PackageTreeExists(string rootPath)
        {
            if (!Directory.Exists(rootPath))
                return false;

            return RootDirectoryContainsBuildFiles(rootPath) > 0;
        }

        private int RootDirectoryContainsBuildFiles(string rootPath)
        {
            //HACK: Basic check for now.  Could be expanded for a core set of required build.boo files
            return (Directory.GetFiles(rootPath, "build.boo", SearchOption.AllDirectories).Length);
        }

        public MetaDataSynchroniser(SourceControl sourceControl)
        {
            this.sourceControl = sourceControl;
        }
    }

}