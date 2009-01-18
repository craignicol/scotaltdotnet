using System;
using System.IO;
using Horn.Core.Utils.Framework;
using Xunit;

namespace Horn.Core.Spec.Integration.Utils
{
    public class When_Version_35_Is_Requested : Specification
    {
        protected override void Because()
        {
        }

        [Fact]
        public void Then_Framework_35_Path_Is_Returned()
        {
            Console.WriteLine(FrameworkLocator.Instance[FrameworkVersion.frameworkVersion35].MSBuild.AssemblyPath);

            Assert.True(File.Exists(FrameworkLocator.Instance[FrameworkVersion.frameworkVersion35].MSBuild.AssemblyPath));
        }
    }
}