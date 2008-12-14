using System;
using System.IO;

namespace Horn.Core.Spec.Integration.Get
{
    public class SourceControlTestBase
    {
        public class SubversionTestBase : TestBase
        {
            protected DirectoryInfo tempSandBox = new DirectoryInfo(string.Format("{0}{1}{2}", AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar, Guid.NewGuid()));

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