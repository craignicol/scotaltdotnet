using System.IO;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Framework.helpers;

namespace Horn.Core.Spec.MetaSynchroniserfixture
{
    public abstract class MetaSynchroniserFixtureBase : Specification
    {
        protected IMetaDataSynchroniser metaDataSynchroniser;

        protected SourceControlDouble sourceControlDouble;

        protected override void Before_each_spec()
        {
            sourceControlDouble = new SourceControlDouble("http://www.someurlorsomething.com/");

            metaDataSynchroniser = new MetaDataSynchroniser(sourceControlDouble);
        }
    }
}