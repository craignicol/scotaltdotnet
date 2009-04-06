using Horn.Domain.Dsl;
using Horn.Domain.Framework;
using Horn.Domain.Spec.Unit.dsl;
using Xunit;

namespace Horn.Domain.Spec.BuildEngine
{
    public class When_The_Build_MetaData_Specifies_Nant : BuildWithNantSpecificationBase
    {
        private const string EXPECTED =
            "build release quick rebuild  -t:net-3.5 -buildfile:Horn.build -D:sign=false -D:testrunner=NUnit -D:common.testrunner.enabled=true -D:common.testrunner.failonerror=true -D:build.msbuild=true";

        protected override void Because()
        {
            configReader = factory.Create<BooConfigReader>(@"BuildConfigs/Horn/hornnant.boo");
            configReader.Prepare(); 
        }

        [Fact]
        public void Then_The_Nant_Build_Tool_Generates_The_Correct_Command_Line_Parameters()
        {
            IBuildTool nant = configReader.BuildEngine.BuildTool;

            var cmdLineArgs = nant.CommandLineArguments("Horn.build", configReader.BuildEngine, packageTree,
                                                        FrameworkVersion.FrameworkVersion35).Trim();

            Assert.Equal(EXPECTED, cmdLineArgs);
        }
    }
}