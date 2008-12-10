using Horn.Core.SCM;
using Xunit;

namespace Horn.Core.Spec.Integration.Get
{
    public class When_Horn_Is_In_The_BuildMetaData : SourceControlTestBase.SubversionTestBase
    {
        public const string HORN_URL = "https://scotaltdotnet.googlecode.com/svn/trunk";

        protected override void Because()
        {
            SetUpTemporarySandBox();
        }

        protected override void After_each_spec()
        {
            DeleteTempSandBox();
        }

        [Fact]
        public void Then_The_Source_Horn_Is_Downloaded()
        {
            SourceControl svn = new SVNSourceControl(HORN_URL);

            svn.Export(tempSandBox.FullName);

            Assert.Equal(2, tempSandBox.GetDirectories().Length);
        }
    }
}