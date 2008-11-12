using System;
using Horn.Core.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    using Rhino.Mocks;

    public class When_The_Build_Config_Reader_Receives_A_Request_For_A_Component : BaseDSLSpecification
    {
        private IBuildConfigReader reader;
        private IDependencyResolver dependencyResolver;

        protected override void Before_each_spec()
        {
            dependencyResolver = CreateStub<IDependencyResolver>();
            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>())
                .Return(new SVNSourceControl(string.Empty));

            IoC.InitializeWith(dependencyResolver);
        }

        protected override void Because()
        {
            reader = new BuildConfigReaderDouble();
        }

        [Fact]
        public void Then_The_Congig_Reader_Returns_The_Ceorrect_MetaData()
        {
            BuildMetaData metaData = reader.GetBuildMetaData("horn");

            AssertBuildMetaDataValues(metaData);
        }
    }

    public class When_The_Build_Config_Reader_Receives_An_Empty_String : BaseDSLSpecification
    {
        private IBuildConfigReader reader;

        protected override void Because()
        {
            reader = new BuildConfigReaderDouble();
        }

        [Fact]
        public void Then_The_Congig_Reader_Throws_A_Custom_Exception()
        {
            Assert.Throws<UnknownBuildComponentException>(() => reader.GetBuildMetaData(""));
        }
    }

    public class When_The_Build_Config_Reader_Receives_An_Unkonwn_Component : BaseDSLSpecification
    {
        private IBuildConfigReader reader;

        protected override void Because()
        {
            reader = new UnkownBuildConfigReaderDouble();
        }

        [Fact]
        public void Then_The_Config_Reader_Throws_A_Custom_Exception()
        {
            Assert.Throws<UnknownBuildComponentException>(() => reader.GetBuildMetaData("thisdoexnotexist"));
        }
    }

    public class UnkownBuildConfigReaderDouble : BuildConfigReader
    {
        protected override string GetBuildFile(string sourceName)
        {
            return @"boo/projects/thisdoesnotexist.boo";
        }
    }

    public class BuildConfigReaderDouble : BuildConfigReader
    {
        protected override string GetBuildFile(string sourceName)
        {
            return @"boo/projects/hornconfig.boo";
        }
    }
}