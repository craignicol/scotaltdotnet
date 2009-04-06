using System;
using Horn.Core.extensions;
using Horn.Framework.helpers;
using Xunit;

namespace Horn.Domain.Spec.Extensions
{
    public class When_Given_A_String : Specification
    {
        private string actual;

        protected override void Because()
        {
            actual = DirectoryHelper.GetBaseDirectory();
        }

        [Fact]
        public void Then_The_Debug_Folders_Will_Be_Removed()
        {
            Assert.DoesNotContain("\\bin\\Debug", actual.RemoveDebugFolderParts());
        }
    }
}