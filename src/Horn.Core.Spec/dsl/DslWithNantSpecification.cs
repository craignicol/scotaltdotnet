using Horn.Core.dsl;
using Horn.Core.PackageStructure;
using Horn.Core.Utils.Framework;
using Xunit;

namespace Horn.Core.Spec.Unit.dsl
{
    public class When_Nant_Is_Specified_In_The_Dsl_As_The_Build_Tool : BuildWithNantSpecificationBase
    {
        protected override void Because()
        {
            configReader = factory.Create<BaseConfigReader>(@"BuildConfigs/Horn/buildnant.boo");
            configReader.Prepare(); 
        }

        [Fact]
        public void Then_The_Dsl_Compiles()
        {
            Assert.IsAssignableFrom<NAntBuildTool>(configReader.BuildEngine.BuildTool);

            Assert.Equal(1, configReader.BuildEngine.Tasks.Count);

            Assert.Equal("build", configReader.BuildEngine.Tasks[0]);

            Assert.Equal(5, configReader.BuildEngine.Parameters.Count);
        }
    }

    public class When_Nant_Is_The_Build_Tool_And_Command_Line_Arguments_Are_Requested : BuildWithNantSpecificationBase
    {
        private IBuildTool buildTool;

        private const string EXPECTED_CMD_LINE_ARGUMENTS =
            " -t:net-3.5 -buildfile:Horn.build -D:sign=false -D:testrunner=NUnit -D:common.testrunner.enabled=true -D:common.testrunner.failonerror=true -D:build.msbuild=true ";
        protected override void Because()
        {
            configReader = factory.Create<BaseConfigReader>(@"BuildConfigs/Horn/buildnant.boo");
            configReader.Prepare(); 

            buildTool = configReader.BuildEngine.BuildTool;
        }

        [Fact]
        public void Then_The_Build_Tool_Renders_The_Expected_Arguments()
        {
            var actual = buildTool.CommandLineArguments("Horn.build", configReader.BuildEngine, packageTree,
                                                        FrameworkVersion.frameworkVersion35);

            Assert.Equal(EXPECTED_CMD_LINE_ARGUMENTS, actual);
        }
    }
}