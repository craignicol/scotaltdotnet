using System;
using System.IO;
using Horn.Framework.helpers;

namespace Horn.Core.Spec.Integration.Get
{
    public class SourceControlTestBase
    {
        public class SubversionTestBase : TestBase
        {
            protected DirectoryInfo tempSandBox = new DirectoryInfo(DirectoryHelper.GetTempDirectoryName());

            protected override void Because()
            {
            }

            protected void SetUpTemporarySandBox()
            {
                DeleteTempSandBox();
            }

            protected void DeleteTempSandBox()
            {
                if (tempSandBox.Exists)
                    tempSandBox.Delete(true);
            }
        }        
    }
}