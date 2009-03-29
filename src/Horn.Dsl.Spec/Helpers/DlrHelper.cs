using Horn.Core;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Core.Utils.Framework;
using IronRuby;
using Xunit;

namespace Horn.Dsl.Spec.Helpers
{
    public static class DlrHelper
    {
        public static BuildMetaData RetrieveBuildMetaDataFromTheDlr(string buildFilePath, string variableName, string methodName)
        {
            var engine = Ruby.CreateEngine();

            engine.Runtime.LoadAssembly(typeof(BuildMetaData).Assembly);

            engine.ExecuteFile(buildFilePath);

            var klass = engine.Runtime.Globals.GetVariable(variableName);

            var instance = engine.Operations.CreateInstance(klass);

            return  (BuildMetaData)engine.Operations.InvokeMember(instance, methodName);
        }

        public static void AssertBuildMetaData(IBuildMetaData buildMetaData)
        {
            //Assert.Equal("A .NET build and dependency manager", buildMetaData.Description);

            Assert.IsAssignableFrom<SVNSourceControl>(buildMetaData.SourceControl);

            Assert.Equal("src/horn.sln", buildMetaData.BuildEngine.BuildFile);

            Assert.Equal(FrameworkVersion.FrameworkVersion35, buildMetaData.BuildEngine.Version);

            Assert.IsAssignableFrom<MSBuildBuildTool>(buildMetaData.BuildEngine.BuildTool);

            //Assert.Equal(buildMetaData.ProjectInfo["homepage"].ToString(), "http://code.google.com/p/scotaltdotnet/");

            Assert.Equal(buildMetaData.BuildEngine.OutputDirectory, "Output");

            Assert.True(buildMetaData.BuildEngine.Dependencies.Count > 0);
        }
    }
}