using System.IO;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Framework.helpers;

namespace Horn.Core.Spec.MetaSynchroniserfixture
{
    public class MetaSynchroniserFixtureBase : Specification
    {
        protected IMetaDataSynchroniser metaDataSynchroniser;

        protected string rootPath;

        protected SourceControlDouble sourceControlDouble;

        protected override void Before_each_spec()
        {
            sourceControlDouble = new SourceControlDouble("http://www.someurlorsomething.com/");

            metaDataSynchroniser = new MetaDataSynchroniser(sourceControlDouble);

            rootPath = DirectoryHelper.GetTempDirectoryName();
        }

        protected override void After_each_spec()
        {
            if (Directory.Exists(rootPath))
                Directory.Delete(rootPath, true);
        }

        protected override void Because()
        {
        }
    }
}