using System;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.Unit.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.HornTree
{
    public class When_resolving_a_boo_build_file : BaseDSLSpecification
    {
        private IBuildFileExtensionResolver fileExtensionResolver;

        private DirectoryInfo buildFolder;

        protected override void Before_each_spec()
        {
            fileExtensionResolver = new BuildFileExtensionResolver();
        }

        protected override void Because()
        {
            buildFolder = GetTestBuildConfigsFolder();
        }

        [Fact]
        public void Then_a_boo_extension_is_returned_from_the_resolver()
        {
            Assert.Equal("boo", fileExtensionResolver.Resolve(buildFolder));
        }
    }
}