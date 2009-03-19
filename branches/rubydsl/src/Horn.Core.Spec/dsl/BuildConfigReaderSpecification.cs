using System;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Framework.helpers;
using Horn.Spec.Framework.Extensions;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    using Rhino.Mocks;

    public class When_The_Build_Config_Reader_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>())
                .Return(new SourceControlDouble(string.Empty));

            IoC.InitializeWith(dependencyResolver);

            rootDirectory = new DirectoryInfo(string.Format("{0}\\BuildConfigs\\Horn", DirectoryHelper.GetBaseDirectory().ToLower().ResolvePath()));
        }

        protected override void Because()
        {
            reader = new BuildConfigReader();
        }

        [Fact]
        public void Then_The_Config_Reader_Returns_The_Correct_MetaData()
        {
            var metaData = reader.SetDslFactory(rootDirectory).GetBuildMetaData();

            AssertBuildMetaDataValues(metaData);
        }
    }

    public class When_SetDslFactory_Is_Not_Set : BaseDSLSpecification
    {
        protected override void Because()
        {
            reader = new BuildConfigReader();
        }

        [Fact]
        public void Then_An_Argument_Null_Exception_Is_Thrown()
        {
            Assert.Throws<ArgumentNullException>(() => reader.GetBuildMetaData());
        }
    }

    public class When_The_Build_File_Does_Not_Exist : BaseDSLSpecification
    {
        protected override void Because()
        {
            var directoryWithNoBooFile = Path.Combine(DirectoryHelper.GetBaseDirectory(), "nonexistent");

            if (!Directory.Exists(directoryWithNoBooFile))
                Directory.CreateDirectory(directoryWithNoBooFile);

            rootDirectory = new DirectoryInfo(directoryWithNoBooFile);

            reader = new BuildConfigReader();
        }

        [Fact]
        public void Then_The_Config_Reader_Throws_A_Custom_Exception()
        {
            Assert.Throws<MissingBuildFileException>(() => reader.SetDslFactory(rootDirectory).GetBuildMetaData());
        }
    }
}