using Horn.Core.dsl;
using Horn.Core.Package;
using Xunit;

namespace Horn.Core.Spec.Integration
{
    public class When_An_IBuildConfigReader_Is_Requested_From_The_IoC : IoCSpecificationBase
    {
        protected override void Because()
        {
        }

        [Fact]
        public void Then_A_Build_Config_Reader_Is_Returned()
        {
            Assert.IsAssignableFrom<IBuildConfigReader>(IoC.Resolve<IBuildConfigReader>());
        }
    }

    public class When_A_SourceControl_Is_Requested_From_The_IoC : IoCSpecificationBase
    {
        protected override void Because()
        {
        }

        [Fact]
        public void Then_The_Build_Config_Reader_Is_Returned()
        {
            Assert.IsAssignableFrom<SVNSourceControl>(IoC.Resolve<SVNSourceControl>());
        }
    }

    public class When_An_Install_Key_Is_Passed_To_The_Container : IoCSpecificationBase
    {
        protected override void Because()
        {
        }

        [Fact]
        public void Then_A_Package_Builder_Is_Returned()
        {
            Assert.IsAssignableFrom<PackageBuilder>(IoC.Resolve<IPackageCommand>("install"));
        }
    }

}